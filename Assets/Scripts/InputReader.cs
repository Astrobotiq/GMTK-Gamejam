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

        if (inputs.CharacterControls.Throw.IsPressed())
        {
            OnThrowEvent.Invoke(true);
        }
        else
        {
            OnThrowEvent.Invoke(false);
        }

        if (inputs.CharacterControls.Hold.IsPressed())
        {
            OnHoldEvent.Invoke(true);
        }
        else
        {
            OnHoldEvent.Invoke(false); 
        }

        if (inputs.CharacterControls.RightOxygen.IsPressed())
        {
            OnRightOxygenEvent.Invoke(true);
        }
        else
        {
            OnRightOxygenEvent.Invoke(false);
        }
        
        if (inputs.CharacterControls.LeftOxygen.IsPressed())
        {
            OnLeftOxygenEvent.Invoke(true);
        }
        else
        {
            OnLeftOxygenEvent.Invoke(false);
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
    
    
    
    /*public void onHold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnHoldEvent.Invoke(true);
        }
        else if (context.canceled)
        {
           OnHoldEvent.Invoke(false); 
        }
        
    }*/
}
