using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleManager : MonoBehaviour
{
    Vector3 Destination;
    float duration = 3f;
    Animator animator;
    LevelManager levelManager;
    void Start()
    {
        Destination = new Vector3(transform.position.x + 5f, transform.position.y + 5f, transform.position.z);
        animator = GetComponent<Animator>();
        levelManager = GameObject.FindWithTag("LevelManager").GetComponent<LevelManager>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Character")
        {
            animator.SetBool("hasPlayerEntered",true);
            StartCoroutine(moveToDestination());
            Destroy(other.gameObject);
        }
    }

    IEnumerator moveToDestination()
    {
        float elapsedTime = 0f;

        // Capture the initial position
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            // Update the position
            transform.position = Vector3.Lerp(startingPosition, Destination, elapsedTime / duration);

            // Increment the time
            elapsedTime += Time.deltaTime;

            // Yield and wait for the next frame
            yield return null;
        }

        // Ensure the GameObject ends at point B
        transform.position = Destination;
        levelManager.CompleteLevel();
    }
    
}
