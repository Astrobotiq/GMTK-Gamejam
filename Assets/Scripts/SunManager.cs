using System.Collections;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public Transform sunTransform;         // Reference to the sun's transform
    public float growthRate = 0.1f;        // Rate at which the sun grows over time
    public float maxScale = 3f;            // Maximum scale the sun can reach before exploding
    public float explosionDelay = 2f;      // Time delay before the sun explodes after reaching max scale
    public GameObject explosionEffect;     // Explosion effect to instantiate upon sun's explosion
    public CircleCollider2D triggerBox;       // Trigger box representing the sun's area
    
    
    private bool isExploding = false;      // Flag to check if the sun is in the process of exploding
    private Coroutine growthCoroutine;     // Coroutine for sun growth

    void Start()
    {
        // Start the sun's growth
        growthCoroutine = StartCoroutine(GrowSunOverTime());
    }

    private IEnumerator GrowSunOverTime()
    {
        while (sunTransform.localScale.x < maxScale && !isExploding)
        {
            // Grow the sun uniformly in all directions
            sunTransform.localScale += Vector3.one * growthRate * Time.deltaTime;

            // Visual clue: Change the sun's color intensity based on its size
            float intensity = Mathf.InverseLerp(1f, maxScale, sunTransform.localScale.x);
            sunTransform.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.yellow, Color.red, intensity);

            yield return null; // Wait for the next frame
        }

        if (sunTransform.localScale.x >= maxScale)
        {
            // Sun has reached its maximum size, trigger the explosion
            StartCoroutine(TriggerExplosion());
        }
    }

    private IEnumerator TriggerExplosion()
    {
        isExploding = true;

        // Optional: Add some pre-explosion effects or delays here if needed
        // Instantiate the explosion effect at the sun's position
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, sunTransform.position, Quaternion.identity);
        }

        // Trigger game over (handled by the GameManager)
        //GameManager.Instance.OnGameOver();

        // Destroy the sun object or disable it
        Destroy(sunTransform.gameObject);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        // Check if the player has entered the sun's trigger box (could add logic if needed)
        if (!isExploding)
        {
            
            Destroy(collision.gameObject);
            // Maybe trigger some warning or visual effect to show the sun is too close
        }
       
    }

    public void StopSunGrowth()
    {
        // Stop the sun's growth (could be called if the player wins)
        if (growthCoroutine != null)
        {
            StopCoroutine(growthCoroutine);
        }
    }
}

