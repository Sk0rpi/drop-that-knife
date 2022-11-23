using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckerMovement : MonoBehaviour
{
    private TriggerValue _triggerValue;

    private void Awake()
    {
        if (GetComponent(typeof(TriggerValue)) != null)
        {
            _triggerValue = GetComponent(typeof(TriggerValue)) as TriggerValue;
        }
    }
    private void OnEnable()
    {
        GameController.instance.onBlinkPerformed.AddListener(ResetTrigger);
    }

    private void OnDisable()
    {
        GameController.instance.onBlinkPerformed.RemoveListener(ResetTrigger);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _triggerValue.triggered = true;
        }
    }

    private void ResetTrigger()
    {
        _triggerValue.triggered = false;
    }

}
