using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
  /*  //Character stats
    public float oxygenRotationSpeed =0.6f;
    public Vector3 feetOffset = new Vector3(0, -0.75f);
    
    bool isHoldPressed;
    bool isLeftBlowPressed;
    bool isRightBlowPressed;
    bool isThrowPressed;
    public float rayDistance = 10;
    public LayerMask raycastLayer;
    public float maxVelocity = 5;

    Transform chosenArm;
    Vector2 dir;
    Rigidbody2D rb;
    public Transform rightHand;
    public Transform leftHand;
    
    //Hold action requirements
    public ThrowableHandler feetHandler;
    public ThrowableHandler bodyHandler;
    public Animator animator;
    private bool isHoldingLittle;
    private bool isHoldingBig;
    bool isHoldingMeteor;
    float meteorAngle;
    float meteorRadius;
    public ArrowIndicator arrowIndicator;
    private Vector3 arrowDirection;
    public Throwable selectedThrowable;
    


    void OnEnable()
    {
        InputReader.OnHoldEvent += setHolding;
        InputReader.OnLeftOxygenEvent += setLeftBlowing;
        InputReader.OnRightOxygenEvent += setRightBlowing;
        InputReader.OnThrowEvent += setThrow;
    }

    void OnDisable()
    {
        InputReader.OnHoldEvent -= setHolding;
        InputReader.OnLeftOxygenEvent -= setLeftBlowing;
        InputReader.OnRightOxygenEvent -= setRightBlowing;
        InputReader.OnThrowEvent -= setThrow;
    }

    public void setHolding(bool isPressed)
    {
        isHoldPressed = isPressed;
    }
    
    public void setLeftBlowing(bool isPressed)
    {
        isLeftBlowPressed = isPressed;
    }
    
    public void setRightBlowing(bool isPressed)
    {
        isRightBlowPressed = isPressed;
    }

    public void setThrow(bool isPressed)
    {
        isThrowPressed = isPressed;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        selectedThrowable = null;
        isHoldPressed = false;
        isLeftBlowPressed = false;
        isRightBlowPressed = false;
        isThrowPressed = false;
        isHoldingLittle = false;
        isHoldingBig = false;
    }

    void Update()
    {
        if (isHoldPressed && isThrowPressed)
        {
            HandleThrow();       
        }
        else if(isHoldPressed)
        {
            HandleHold();
        }
        else
        {
            HandleDrift();
        }

        var x = rb.velocity.x;
        var y = rb.velocity.y;

        if (x>maxVelocity)
        {
            x = maxVelocity;
        }else if (x < -maxVelocity)
        {
            x = -maxVelocity;
        }

        if (y>maxVelocity)
        {
            y = maxVelocity;
        }else if (y < -maxVelocity)
        {
            y = -maxVelocity;
        }
        rb.velocity = new Vector2(x, y);
    }

    void HandleThrow()
    {
        if (selectedThrowable != null)
        {
            // Extract the Z rotation angle in degrees and adjust for the 90-degree offset
            float angleInDegrees = arrowIndicator.transform.eulerAngles.z - 90f;

            // Convert the adjusted angle from degrees to radians
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

            // Calculate the direction as a Vector2
            Vector2 throwDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;

            // Debugging ray to visualize the direction
            Debug.DrawRay(transform.position, throwDirection * 3, Color.red, 2.0f);

            // Apply force to the throwable object
            selectedThrowable.ApplyForce(-throwDirection * 3);

            // Apply opposite force to the character
            rb.AddForce(throwDirection * 3, ForceMode2D.Impulse);

            // Reset the selected throwable
            selectedThrowable = null;
            arrowIndicator.gameObject.SetActive(false);
        }
    }

    void HandleHold()
    {
        var bodyThrowable = bodyHandler.getSelected();
        var feetThrowable = feetHandler.getSelected();


        if (feetThrowable != null)
        {
            if (selectedThrowable == null)
            {
                Debug.Log("Big holding animasyon");
                isHoldingBig = true;
                animator.SetBool("isHoldingBig",isHoldingBig);
            }

            selectedThrowable = feetThrowable.GetComponent<Throwable>();
            HandleFeetHold(feetThrowable);
            return;
        }
        else if(bodyThrowable != null)
        {
            isHoldingLittle = true;
            animator.SetBool("isHoldingLittle",isHoldingLittle);
            HandleBodyHold(bodyThrowable);
        }
    }

    void HandleFeetHold(GameObject go)
    {
        Vector3 chestPosition = transform.position;

// Offset from chest to feet (adjust based on character's height)
        feetOffset = new Vector2(0, -0.75f); // Adjust this based on your character's dimensions

// Calculate the position of the feet
        Vector3 feetPosition = chestPosition + (Vector3)feetOffset;

// Collider of the target GameObject
        Collider2D targetCollider = go.GetComponent<Collider2D>();

// Get the closest point on the target's collider
        Vector3 closestPoint = targetCollider.ClosestPoint(feetPosition);


// Adjust the character's position so that the feet are at the closest point
        transform.position = closestPoint - (Vector3)feetOffset;

// Perform a raycast from the closest point to determine the surface normal
        RaycastHit2D hit = Physics2D.Raycast(closestPoint, Vector2.down, 1f,raycastLayer);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            // Get the normal of the surface at the closest point
            Vector2 surfaceNormal = hit.normal;

            // Calculate the angle to align the character with the surface normal
            float angle = Mathf.Atan2(surfaceNormal.y, surfaceNormal.x) * Mathf.Rad2Deg;

            // Set the rotation of the character to align with the surface normal
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

            // Optional: Debugging visualization
            Debug.DrawRay(hit.point, surfaceNormal * 2, Color.red, 2.0f);
        }
        else
        {
            Debug.LogWarning("Raycast did not hit the target collider.");
        }

    }

    void HandleBodyHold(GameObject ob)
    {
        if (ob.GetComponent<Meteor>() != null)
        {
            rb.velocity = Vector2.zero; // Reset velocity
            isHoldingMeteor = true;
            transform.position = ob.transform.position;
            var meteor = ob.GetComponent<Meteor>();

            if (meteor.isRotatingMeteor)
            {
                meteorAngle = arrowIndicator.transform.eulerAngles.z - 90f;

                // Convert the adjusted angle from degrees to radians
                meteorAngle = meteorAngle * Mathf.Deg2Rad;
                var direction = new Vector2(Mathf.Cos(meteorAngle), Mathf.Sin(meteorAngle));
                direction = -direction;
                Debug.DrawRay(transform.position, direction * 3, Color.red, 2.0f);
                transform.eulerAngles = new Vector3(0, 0, ob.transform.eulerAngles.z);
            }
            else if (meteor.isSpinningMeteor)
            {
                meteorAngle = meteor.angle;
                meteorRadius = meteor.spinningRadius;

                // Calculate the velocity needed for spinning motion
                float speed = meteor.spinningSpeed * 5f;

                // Create a velocity vector based on the meteor angle
                Vector2 velocity = new Vector2(Mathf.Cos(meteorAngle), -Mathf.Sin(meteorAngle)) * speed;

                // Rotate the velocity vector to align with the character's head direction
                float headAngle = transform.eulerAngles.z; // Assuming the character's head direction is aligned with its rotation
                float headAngleRad = headAngle * Mathf.Deg2Rad;

                // Rotate the velocity by the head angle
                Vector2 rotatedVelocity = new Vector2(
                    velocity.x * Mathf.Cos(headAngleRad) - velocity.y * Mathf.Sin(headAngleRad),
                    velocity.x * Mathf.Sin(headAngleRad) + velocity.y * Mathf.Cos(headAngleRad)
                );

                // Apply the rotated velocity to the Rigidbody
                rb.velocity = rotatedVelocity;
            }
        }
        else if (ob.GetComponent<Throwable>() != null)
        {
            rb.velocity = Vector2.zero;
            if (selectedThrowable == null || selectedThrowable != ob.GetComponent<Throwable>())
            {
                arrowIndicator.gameObject.SetActive(true);
                selectedThrowable = ob.GetComponent<Throwable>();
            }
        }
    }


    void HandleDrift()
    {
        if (isHoldingLittle)
        {
            isHoldingLittle = false;
            animator.SetBool("isHoldingLittle",isHoldingLittle);
            if (isHoldingMeteor)
            {
                isHoldingMeteor = false;
                var direction = new Vector2(Mathf.Cos(meteorAngle),Mathf.Sin(meteorAngle));
                direction = -direction;
                Debug.DrawRay(transform.position, direction * 3, Color.red, 2.0f);
                rb.AddForce(direction*10,ForceMode2D.Impulse);
            }
        }
        if (isHoldingBig)
        {
            isHoldingBig = false;
            animator.SetBool("isHoldingBig",isHoldingBig);
            var angle= transform.eulerAngles.z - 90f;

            // Convert the adjusted angle from degrees to radians
            angle = angle * Mathf.Deg2Rad;
            var direction = new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
            direction = -direction;
            rb.AddForce(direction*3,ForceMode2D.Impulse);
            selectedThrowable = null;
        }
        if (isRightBlowPressed)
        {
            RightOxygen();
        }

        if (isLeftBlowPressed)
        {
            LeftOxygen();
        }
    }

    // Update is called once per frame
    /*public void Look(Vector2 dir){
        if (dir.x > 0)
        {
            head.rotation = new Quaternion(0f,0f,0f,0f);
            
        }
        else if (dir.x < 0)
        {
            head.rotation = new Quaternion(0f,180f,0f,0f);
            
        }
        
    }*/

    
    
    /*private void CastRayFromArm(Transform arm, Vector2 direction)
    {
        // Check if the arm transform is assigned
        if (arm == null) return;

        // Define the starting point of the ray (from the arm's position)
        Vector2 rayOrigin = arm.position;

        // Draw the ray in the Scene view for debugging purposes
        Debug.DrawRay(rayOrigin, direction * rayDistance, Color.red);

        // Perform the raycast in 2D
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayDistance, raycastLayer);

        if (hit.collider != null)
        {
            // Log the name of the object that the ray hit
            Debug.Log("Hit: " + hit.collider.name);

            // You can add more logic here, such as interacting with the hit object
        }
    }

    

    public void LeftOxygen()
    {
        //transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z - 0.2f);

        // Convert the angle to radians (for trigonometric functions)
        float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;

        // Create a Vector2 direction from the rotation angle
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // For demonstration: print the direction
        Debug.DrawRay(leftHand.position,direction,Color.red);

        rb.AddForceAtPosition(direction*oxygenRotationSpeed,leftHand.position,ForceMode2D.Force);
    }
    
    public void RightOxygen()
    {
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 0.2f);
        
        // Convert the angle to radians (for trigonometric functions)
        float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;

        // Create a Vector2 direction from the rotation angle
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // For demonstration: print the direction
        Debug.DrawRay(rightHand.position,direction,Color.red);

        rb.AddForceAtPosition(-direction*oxygenRotationSpeed,rightHand.position,ForceMode2D.Force);
    }
    
    */
}
