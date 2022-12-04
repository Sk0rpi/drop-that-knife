using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerConnector : MonoBehaviour
{
    [Header("What is this trigger for? (only dev info)")]
    public string triggerUsage;
    [Space]
    [Header("Time Trigger")]
    public bool isTimeTrigger;
    public int timeInSec;
    [Space]
    [Header("Object Trigger")]
    public bool isObjectTrigger;
    public GameObject toObserve;

    [Space]
    [Header("Button Trigger")]
    [Tooltip("If the button is a button trigger, " +
        "to observe should be the same object and this object should have " +
        "a TriggerCheckerInput component")]
    public bool isButtonTrigger;

    [Space]
    [Header("Blink Delay (real time)")]
    public float blinkDelay = 0f;

    [Space]
    [Header("Where does this trigger leads?")]
    public GameObject nextBlink;

    [Space]
    [Header("Change the position of player?")]
    public bool movePlayer = false;
    public Transform newPosition;

    private void Start()    // !! DANGEROUS !! Button trigger and object trigger are not compatible
    {
        if(isButtonTrigger)
        {
            toObserve = gameObject;
        }
    }
}
