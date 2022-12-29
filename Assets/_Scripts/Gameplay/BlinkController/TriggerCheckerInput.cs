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
        GameController.instance.onBlinkPerformed.AddListener(ResetTrigger);
    }
    private void OnDisable()
    {
        buttonToAdvance.action.performed -= SetTrigger;
        GameController.instance.onBlinkPerformed.RemoveListener(ResetTrigger);
    }

    private void SetTrigger(InputAction.CallbackContext context) 
    {
        triggered = true;
    }

    private void ResetTrigger()
    {
        triggered = false;
    }

}
