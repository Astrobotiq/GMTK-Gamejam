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
    public OxygenManager oxygenManager;

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

        if (inputs.CharacterControls.RightOxygen.IsPressed())
        {
            controller.RightOxygen();
            oxygenManager.StartUseO2ForPush();
        }
        else
        {
            oxygenManager.EndUseO2ForPush();
        }
        
        if (inputs.CharacterControls.LeftOxygen.IsPressed())
        {
            controller.LeftOxygen();
            oxygenManager.StartUseO2ForPush();
        }
        else
        {
            oxygenManager.EndUseO2ForPush();
        }

        
        
    }

    //public void onLeftOxygen(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
    // //       controller.LeftOxygen();
    //    }
    //    else if (context.canceled)
    //    {
    //        
    //    }
    //}
    //
    //public void onRightOxygen(InputAction.CallbackContext context)
    //{
    //    if (context.performed)
    //    {
   //         
    //    }
    //    else if (context.canceled)
    //    {
    //        
   //     }
    //}
    
    
    
    public void onHold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.Hold();
        }
    }
}
