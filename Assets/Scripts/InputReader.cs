using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    CharacterController controller;
    CharacterInputs inputs;
    Vector2 Rotation;

    void Start()
    {
        inputs = new CharacterInputs();
        inputs.CharacterControls.Enable();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Rotation = inputs.CharacterControls.Look.ReadValue<Vector2>();
        controller.Look(Rotation);
    }

    public void onLeftOxygen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Left ox");
        }
    }
    
    public void onRightOxygen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Right ox");
        }
    }
    
    
    
    public void onHold(InputAction.CallbackContext context)
    {
        
    }
}