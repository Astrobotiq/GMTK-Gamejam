using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 0f; 
    public float rotationSpeed = 50f; 
    public float spinningSpeed = 2f; // Speed of spinning in degrees per second
    public bool isRotatingMeteor = false;
    public bool isSpinningMeteor = false;
    public float degree;
    private Vector3 initialPosition;

    private Vector3 direction; // Only used for moving meteor
    public float spinningRadius = 3f; // Radius of the spinning circle
    public float angle; // Current angle for spinning

    private void Start()
    {
        initialPosition = transform.position;
        if (isRotatingMeteor || isSpinningMeteor)
        {
            // Initialize spinning properties if necessary
        }
        else
        {
            // Initialize direction for moving meteors
            direction = Vector3.right;
        }
    }

    private void Update()
    {
        if (isSpinningMeteor)
        {
            // Spin around in circles
            angle += spinningSpeed * Time.deltaTime;
            float x = Mathf.Cos(angle) * spinningRadius;
            float y = Mathf.Sin(angle) * spinningRadius;
            Vector3 newPosition = new Vector3(x, y, transform.position.z)+ initialPosition;
            
            // Set the new position
            transform.position = newPosition;

            // Calculate the direction from the meteor to the center (initial position)
            Vector3 directionFromCenter = transform.position - initialPosition;

            // Calculate the rotation angle so the opposite side faces the center
            float angleFromCenter = Mathf.Atan2(directionFromCenter.y, directionFromCenter.x) * Mathf.Rad2Deg;

            // Rotate the meteor so the opposite side faces the center
            transform.rotation = Quaternion.Euler(0, 0, angleFromCenter + degree); // Adjust +90f if needed
        }
        else if (isRotatingMeteor)
        {
            // Rotate around its own axis
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Move the meteor in the specified direction
            transform.Translate(direction * speed * Time.deltaTime, Space.Self);
        }
    }

    private bool IsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}