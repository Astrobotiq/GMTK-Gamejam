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
    
    // Start is called before the first frame update
    void Start()
    {
        isHolding = false;
        rb = GetComponent<Rigidbody2D>();
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
        
    }

    public void Hold()
    {
        //Throwing ray from my character
        float zRotation = transform.eulerAngles.z - zOffset;
        float radians = zRotation * Mathf.Deg2Rad;
        RaycastHit2D hit = fireRayCast(radians, transform.position, rayDistance);
        if (hit.rigidbody != null)
        {
            isHolding = true;
            Debug.Log(hit.point);
            Debug.DrawRay(hit.point,hit.normal*3,Color.green,2f);
            //rotating character through surface
            transform.eulerAngles = calculateDegree(hit.normal);
            //positioning character according to hit point and value offset
            transform.position = calculatePosition(hit.point,yOffset);
        }
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
            Release();
        }
    }

    public void Release()
    {
        isHolding = false;
    }
}
