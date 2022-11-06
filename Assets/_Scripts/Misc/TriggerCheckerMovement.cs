using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckerMovement : MonoBehaviour
{
    private TriggerValue _triggerValue;
    private void Start()
    {
        if (GetComponent(typeof(TriggerValue)) != null)
        {
            _triggerValue = GetComponent(typeof(TriggerValue)) as TriggerValue;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _triggerValue.triggered = true;
        }
    }

}
