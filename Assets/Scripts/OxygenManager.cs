using System.Collections;
using UnityEngine;

public class OxygenManager: MonoBehaviour
{
    private float _oxygenTank = 100f;
    private Coroutine _breatheCoroutine;
    
    void Start()
    {
        Breathe();   
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Oxygen level:"+_oxygenTank);
    }

    public void Use02ForPush(float durationInSeconds)
    {
        StartCoroutine(ConsumeOxygenOverTime(1f, durationInSeconds));
    }

    // Method to start the continuous breathing
    private void Breathe()
    {
        if (_breatheCoroutine == null) // Start only if not already running
        {
            _breatheCoroutine = StartCoroutine(BreatheContinuously());
        }
    }

    // Coroutine to breathe continuously (until oxygen runs out)
    private IEnumerator BreatheContinuously()
    {
        while (_oxygenTank > 0)
        {
            _oxygenTank -= 0.25f * Time.deltaTime;

            if (_oxygenTank <= 0)
            {
                _oxygenTank = 0;
                // Handle oxygen depletion (e.g., game over logic)
                break;
            }

            yield return null; // Wait for the next frame
        }

        _breatheCoroutine = null; // Mark the coroutine as stopped
    }

    // Coroutine to consume oxygen over time (using Time.deltaTime)
    private IEnumerator ConsumeOxygenOverTime(float amountPerSecond, float durationInSeconds)
    {
        float elapsedTime = 0f;

        while (elapsedTime < durationInSeconds && _oxygenTank > 0)
        {
            // Calculate oxygen consumption per frame based on time
            float oxygenConsumption = amountPerSecond * Time.deltaTime;
            _oxygenTank -= oxygenConsumption;

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        if (_oxygenTank <= 0)
        {
            _oxygenTank = 0;
            // Handle oxygen depletion (e.g., game over logic)
        }
    }
}

