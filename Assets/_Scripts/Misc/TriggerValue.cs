using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerValue : MonoBehaviour
{
    public bool triggered = false;

    // Use this function when wanting to trigger the blink through an object
    public void SetTriggered()
    {
        triggered = true;
    }
}
