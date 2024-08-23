using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public delegate void isPressed(bool isPressed);
    public static event isPressed OnHoldEvent;
    public static event isPressed OnThrowEvent;
    public static event isPressed OnRightOxygenEvent;
    public static event isPressed OnLeftOxygenEvent;
    NewCharContr controller;
    CharacterInputs inputs;
    Vector2 Rotation;
    //public OxygenManager oxygenManager;

    void Start()
    {
        inputs = new CharacterInputs();
        inputs.CharacterControls.Enable();
        controller = GetComponent<NewCharContr>();
    }

    void Update()
    {
        Rotation = inputs.CharacterControls.Look.ReadValue<Vector2>();

        if (inputs.CharacterControls.RightOxygen.IsPressed())
        {
            OnRightOxygenEvent.Invoke(true);
        }
      
        
        if (inputs.CharacterControls.LeftOxygen.IsPressed())
        {
            OnLeftOxygenEvent.Invoke(true);
        }
        

        
        
    }

    /*public void onLeftOxygen(InputAction.CallbackContext context)
    {
        if (context.)
        {
            OnLeftOxygenEvent.Invoke(true);
        }
        else if (context.canceled)
        {
            OnLeftOxygenEvent.Invoke(false);
        }
    }

    public void onRightOxygen(InputAction.CallbackContext context)
   {
       if (context.performed)
       {
           OnRightOxygenEvent.Invoke(true);
       }
       else if (context.canceled)
       {
           OnRightOxygenEvent.Invoke(false);
       }
   }*/
    
    
    
    public void onHold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.Hold();
        }
        else if (context.canceled)
        {
           controller.Release();
        }
        
    }

    public void onThrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.Throw();
        }
    }
}
