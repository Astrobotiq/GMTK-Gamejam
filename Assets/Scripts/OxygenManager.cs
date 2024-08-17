using System.Collections;
using UnityEngine;

public class OxygenManager: MonoBehaviour
{
    private float _oxygenTank = 100f;
    private Coroutine _breatheCoroutine;
    private Coroutine _actionCoroutine;
    private bool _stopAction = true;
    
    void Start()
    {
        //Breathe(); 
        Use02ForPush();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Oxygen level:"+_oxygenTank);
        
        

        // Stop action if you click something (e.g., the left mouse button)
        if (Input.GetMouseButtonDown(0))
        {
            _stopAction = false; // Set the flag to stop the action
        }
    }

    public void Use02ForPush()
    {
        if (_actionCoroutine == null) // Ensure only one action is processed at a time
        {
            _actionCoroutine = StartCoroutine(ConsumeOxygenForAction(1f));
        }
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
    private IEnumerator ConsumeOxygenForAction(float amountPerSecond)
    {
        while (_oxygenTank > 0 && !_stopAction)
        {
            // Calculate oxygen consumption per frame based on time
            float oxygenConsumption = amountPerSecond * Time.deltaTime;
            _oxygenTank -= oxygenConsumption;

            if (_oxygenTank <= 0)
            {
                _oxygenTank = 0;
                // Handle oxygen depletion (e.g., game over logic)
                break;
            }

            yield return null; // Wait for the next frame
        }

        _actionCoroutine = null; // Mark the coroutine as stopped
    }
}

