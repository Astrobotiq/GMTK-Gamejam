using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float rotationSpeed = 100f;  // Speed at which the arrow rotates around the player
    public float distanceFromPlayer = 2f;  // Distance from the player's body

    private float currentAngle = 0f;

    void Update()
    {
        // Increment the angle based on the rotation speed
        currentAngle += rotationSpeed * Time.deltaTime;

        // Calculate the position of the arrow based on the angle and distance from the player
        float xOffset = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * distanceFromPlayer;
        float yOffset = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * distanceFromPlayer;

        // Set the new position of the arrow
        transform.position = new Vector3(player.position.x + xOffset, player.position.y + yOffset, transform.position.z);

        // Rotate the arrow to point the bottom towards the player and the top outward
        Vector2 direction = (transform.position - player.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);  // Subtract 90 degrees to align bottom to the player
    }

    public Vector3 getRotation()
    {
        return transform.eulerAngles;
    } 
}
