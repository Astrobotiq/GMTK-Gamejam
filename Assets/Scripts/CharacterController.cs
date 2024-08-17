using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    bool isHolding;
    bool isBlowing;
    bool isLookingLeft;
    public float rayDistance = 10;
    public LayerMask raycastLayer;

    Transform chosenArm;
    public Transform leftArm;
    public Transform rightArm;
    Vector2 dir;
    Rigidbody2D rb;

    

    public Transform head;

    // Start is called before the first frame update
    void Start()
    {
        isLookingLeft = false;
        ChooseArm(isLookingLeft);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Look(Vector2 dir){
        if (dir.x > 0)
        {
            head.rotation = new Quaternion(0f,0f,0f,0f);
            isLookingLeft = false;
            ChooseArm(isLookingLeft);
        }
        else if (dir.x < 0)
        {
            head.rotation = new Quaternion(0f,180f,0f,0f);
            isLookingLeft = true;
            ChooseArm(isLookingLeft);
        }
        
    }

    public void Hold()
    {
        CastRayFromArm(chosenArm,dir);
    }
    
    private void CastRayFromArm(Transform arm, Vector2 direction)
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

    void ChooseArm(bool lookSide)
    {
        if (lookSide)
        {
            chosenArm = leftArm;
            dir = Vector2.left;
        }
        else
        {
            chosenArm = rightArm;
            dir = Vector2.right;
        }
    }

    public void LeftOxygen()
    {
        
    }
    
    public void RightOxygen()
    {
        
    }
    
    
}
