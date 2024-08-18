using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    bool isHoldPressed;
    bool isLeftBlowPressed;
    bool isRightBlowPressed;
    bool isThrowPressed;
    public float rayDistance = 10;
    public LayerMask raycastLayer;

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

    


    void OnEnable()
    {
        InputReader.OnHoldEvent += setHolding;
        InputReader.OnLeftOxygenEvent += setLeftBlowing;
        InputReader.OnRightOxygenEvent += setRightBlowing;
    }

    public void setHolding(bool isPressed)
    {
        Debug.Log("Hold pressed :" + isPressed);
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isHoldPressed = false;
        isLeftBlowPressed = false;
        isRightBlowPressed = false;
        isThrowPressed = false;
        isHoldingLittle = false;
        isHoldingBig = true;
    }

    void Update()
    {
        if (isHoldPressed && isThrowPressed)
        {
            HandleThrow();       
        }
        else if(isHoldPressed)
        {
            Debug.Log("I am gonna handle hold");
            HandleHold();
        }
        else
        {
            HandleDrift();
        }
    }

    void HandleThrow(){}

    void HandleHold()
    {
        var bodyThrowable = bodyHandler.getSelected();
        //var feetThrowable = feetHandler.getSelected();

        if(bodyThrowable != null)
        {
            isHoldingLittle = true;
            animator.SetBool("isHoldingLittle",isHoldingLittle);
            HandleBodyHold(bodyThrowable);
        }
    }

    void HandleBodyHold(GameObject ob)
    {
        
        if (ob.GetComponent<Meteor>()!= null)
        {
            isHoldingMeteor = true;
            transform.position = ob.transform.position;
            var meteor = ob.GetComponent<Meteor>();
            meteorAngle = meteor.angle;
            meteorRadius = meteor.spinningRadius;
        }
        else if(ob.GetComponent<Throwable>() != null)
        {
            
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
                rb.AddForce(direction*10,ForceMode2D.Impulse);
            }
        }
        if (isHoldingBig)
        {
            isHoldingBig = false;
            animator.SetBool("isHoldingBig",isHoldingBig);
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
    }*/

    

    public void LeftOxygen()
    {
        transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z - 0.2f);

        // Convert the angle to radians (for trigonometric functions)
        /*float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;

        // Create a Vector2 direction from the rotation angle
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // For demonstration: print the direction
        Debug.DrawRay(leftHand.position,direction,Color.red);

        rb.AddForceAtPosition(direction*0.2f,leftHand.position,ForceMode2D.Force);*/
    }
    
    public void RightOxygen()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 0.2f);
        /*
        // Convert the angle to radians (for trigonometric functions)
        float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;

        // Create a Vector2 direction from the rotation angle
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));

        // For demonstration: print the direction
        Debug.DrawRay(rightHand.position,direction,Color.red);

        rb.AddForceAtPosition(-direction*0.2f,rightHand.position,ForceMode2D.Force);*/
    }
    
    
}
