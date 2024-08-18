using System;
using System.Collections;
using UnityEngine;

public class OxygenManager: MonoBehaviour
{
    private float _oxygenTank = 100f;
    private Coroutine _breatheCoroutine;
    private Coroutine _oxygenConsumptionCoroutine;
    bool isAlreadyPressed;
    
    void Start()
    {
        isAlreadyPressed = false;
        // Breathe();   
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Oxygen level:"+_oxygenTank);
    }

    void OnEnable()
    {
        InputReader.OnRightOxygenEvent += useOxygen;
        InputReader.OnLeftOxygenEvent += useOxygen;
    }

    void OnDisable()
    {
        InputReader.OnRightOxygenEvent -= useOxygen;
        InputReader.OnLeftOxygenEvent -= useOxygen;
    }

    private void useOxygen(bool isPressed)
    {
        if (isPressed && !isAlreadyPressed)
        {
            isAlreadyPressed = true;
            StartUseO2ForPush();
        }
        else
        {
            isAlreadyPressed = false;
            EndUseO2ForPush();
        }
    }


    public void StartUseO2ForPush()
    {
        // Ensure any existing coroutine is stopped before starting a new one
        if (_oxygenConsumptionCoroutine != null)
        {
            StopCoroutine(_oxygenConsumptionCoroutine);
        }
    
        _oxygenConsumptionCoroutine = StartCoroutine(ConsumeOxygenOverTime(1f));
    }
    public void EndUseO2ForPush()
    {
        // Stop the coroutine if it's running
        if (_oxygenConsumptionCoroutine != null)
        {
            StopCoroutine(_oxygenConsumptionCoroutine);
            _oxygenConsumptionCoroutine = null;
        }
    }
    private IEnumerator ConsumeOxygenOverTime(float amountPerSecond)
    {
        while (_oxygenTank > 0)
        {
            // Calculate oxygen consumption per frame based on time
            float oxygenConsumption = amountPerSecond * Time.deltaTime;
            _oxygenTank -= oxygenConsumption;

            yield return null; // Wait for the next frame
        }

        if (_oxygenTank <= 0)
        {
            _oxygenTank = 0;
            // Handle oxygen depletion (e.g., game over logic)
        }
    
        _oxygenConsumptionCoroutine = null; // Reset coroutine reference when finished
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
    /*private IEnumerator TestOxygenConsumption()
    {
        Debug.Log("Starting test...");

        // Wait for 2 seconds to simulate normal breathing
        yield return new WaitForSeconds(2f);

        // Start using oxygen for push
        Debug.Log("Starting UseO2ForPush...");
        StartUseO2ForPush();

        // Wait for 3 seconds to simulate oxygen consumption for push
        yield return new WaitForSeconds(3f);

        // Stop using oxygen for push
        Debug.Log("Stopping UseO2ForPush...");
        EndUseO2ForPush();

        // Wait for another 3 seconds to observe normal breathing again
        yield return new WaitForSeconds(3f);

        Debug.Log("Test completed.");
    }*/
    
}

