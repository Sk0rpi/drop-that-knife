using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConnector : MonoBehaviour
{
    public bool isTimeTrigger;
    public int timeInSec;
    public bool isObjectTrigger;
    [Tooltip("If the button is a button trigger, " +
        "to observe should be the same object and this object should have " +
        "a TriggerCheckerInput component")]
    public GameObject toObserve;
    [Tooltip("If the button is a button trigger, " +
        "to observe should be the same object and this object should have " +
        "a TriggerCheckerInput component")]
    public bool isButtonTrigger;
    public float blinkDelay = 0f;

    public GameObject nextBlink;

    private void Start()
    {
        if(isButtonTrigger)
        {
            toObserve = gameObject;
        }
    }
}
