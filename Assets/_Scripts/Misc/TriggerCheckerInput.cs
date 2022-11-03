using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerCheckerInput : TriggerValue
{
    public InputActionProperty buttonToAdvance;

    private void OnEnable()
    {
        buttonToAdvance.action.performed += SetTrigger;
    }
    private void OnDisable()
    {
        buttonToAdvance.action.performed -= SetTrigger;
    }

    private void SetTrigger(InputAction.CallbackContext context) 
    {
        triggered = true;
    }
}
