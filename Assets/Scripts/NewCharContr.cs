using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharContr : MonoBehaviour
{
    public float zOffset;
    public float rayDistance = 3f;
    //if you want to learn what to do with this variable look at start function
    public float yOffset;
    public float rotationSpeedMultiplier = 5;
    bool isHolding;
    public bool letRotate;
    Rigidbody2D rb;
    Rigidbody2D holdedRb;
    public Transform leftHand;
    public Transform rightHand;
    public float oxygenRotationSpeed = 0.6f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isHolding = false;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        InputReader.OnRightOxygenEvent += RightO2;
        InputReader.OnLeftOxygenEvent += LeftO2;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isHolding && letRotate)
        {
            var rotationSpeed = rotationSpeedMultiplier * Time.deltaTime;
            var euler = transform.eulerAngles;
            if (euler.z + rotationSpeed>=360)
            {
                transform.eulerAngles = new Vector3(euler.x, euler.y, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(euler.x, euler.y, euler.z + rotationSpeed);
            }
        }
        //nesbe yazdı
        var velocity = rb.velocity;
        if(velocity.x>3)
        {
            velocity.x = 3;
        }
        else if (velocity.x < -3)
        {
            velocity.x = -3;
        }

        if (velocity.y > 3)
        {
            velocity.y = 3;
        }
        else if (velocity.y < -3)
        {
            velocity.y = -3;
        }
        rb.velocity = velocity;
    }

    public void Hold()
    {
        
        //Throwing ray from my character
        float zRotation = transform.eulerAngles.z - zOffset;
        float radians = zRotation * Mathf.Deg2Rad;
        RaycastHit2D hit = fireRayCast(radians, transform.position, rayDistance);
        if (hit.rigidbody != null)
        {
            StopRotate();
            isHolding = true;
            Debug.Log(hit.point);
            Debug.DrawRay(hit.point,hit.normal*3,Color.green,2f);
            //rotating character through surface
            transform.eulerAngles = calculateDegree(hit.normal);
            //positioning character according to hit point and value offset
            transform.position = calculatePosition(hit.point,yOffset);
            //Nesbe Yazdı
            VelocityFunction(hit.rigidbody);
            holdedRb= hit.rigidbody;
        }
    }
    //Nesbe Fonk
    void VelocityFunction(Rigidbody2D obj)
    {
        var commonV= ((rb.velocity*rb.mass)+(obj.velocity*obj.mass)) / (rb.mass+obj.mass);
        Debug.Log(commonV);
        obj.velocity = commonV;
        rb.velocity = commonV;
    }

    void StopRotate()
    {
        rb.angularVelocity = 0f;

    }
    
    Vector3 calculatePosition(Vector2 point, float multiplier)
    {
        float angleInDegrees = transform.eulerAngles.z +90;
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        Vector2 offset = new Vector2(multiplier * Mathf.Cos(angleInRadians), multiplier * Mathf.Sin(angleInRadians));
        return point + offset;
    }

    //This function calculate and cast a ray from given position with given distance. 
    RaycastHit2D fireRayCast(float radians, Vector3 position, float distance = 3f)
    {
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        Debug.DrawRay(position, direction * distance, Color.green,2f);
        return Physics2D.Raycast(position, direction, distance);
    }

    Vector3 calculateDegree(Vector2 normal)
    {
        // Calculate the angle in radians using Atan2
        float angleRadians = Mathf.Atan2(normal.y, normal.x);

        // Convert radians to degrees
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        return new Vector3(0, 0, angleDegrees-90);
    }

    public void Throw()
    {
        if (isHolding)
        {
            float angleInDegrees = transform.eulerAngles.z +90;
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            rb.AddForce(dir,ForceMode2D.Impulse);
            holdedRb.AddForce(-dir, ForceMode2D.Impulse);
            Debug.Log(holdedRb.velocity);
            Release();
        }
    }

    public void Release()
    {
        isHolding = false;
        holdedRb = null;
    }

    //nesbe
    public void LeftO2(bool isPressed)
    {
        float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
       Debug.DrawRay(leftHand.position,direction,Color.red);
        rb.AddForceAtPosition(direction*oxygenRotationSpeed,leftHand.position,ForceMode2D.Force);
        Debug.Log("left");
    }
    public void RightO2(bool isPressed)
    {
        
        float angleInRadians = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
       Debug.DrawRay(rightHand.position,direction,Color.red);
        rb.AddForceAtPosition(-direction*oxygenRotationSpeed,rightHand.position,ForceMode2D.Force);
        Debug.Log("right");
    }
    
}
