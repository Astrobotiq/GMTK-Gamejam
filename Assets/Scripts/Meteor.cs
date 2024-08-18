using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 0f; 
    public float rotationSpeed = 50f; 
    public float spinningSpeed = 2f; // Speed of spinning in degrees per second
    public bool isRotatingMeteor = false;
    public bool isSpinningMeteor = false;

    private Vector3 direction; // Only used for moving meteor
    private float spinningRadius = 3f; // Radius of the spinning circle
    private float angle; // Current angle for spinning

    private void Start()
    {
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
            transform.position = new Vector3(x, y, transform.position.z);
        }
        else if (isRotatingMeteor)
        {
            // Rotate around its own axis
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Move the meteor in the specified direction
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
        
        // Destroy meteor if it goes out of view
        if (!IsVisible())
        {
            Destroy(gameObject);
        }
    }

    private bool IsVisible()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}