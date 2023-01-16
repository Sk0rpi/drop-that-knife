using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerConnector : MonoBehaviour
{
    [Header("What is this trigger for? (only dev info)")]
    public string triggerUsage;

    [Space]
    [Header("Trigger Type")]
    public bool isTimeTrigger;
    public int timeInSec;
    [Space]
    public bool isObjectTrigger;
    public GameObject toObserve;
    [Space]
    [Tooltip("If the button is a button trigger, " +
        "to observe should be the same object and this object should have " +
        "a TriggerCheckerInput component")]
    public bool isButtonTrigger;

    [Space]
    [Header("Blink Delay (real time, before the fade out)")]
    public float blinkDelay = 0f;

    [Space]
    [Header("BlackScreenDuration")]
    public float blinkDuration = 0f;

    [Space]
    [Header("Where does this trigger lead?")]
    public GameObject nextBlink;

    [Space]
    [Header("Change the position of player?")]
    public bool movePlayer = false;
    public Transform newPosition;

    [Space]
    [Header("Trail")]
    public bool haveTrail = false;
    public Transform trailTarget;

    [Space] [Header("Flags")]
    public bool setFlagTrue;
    public GameObject flag;

    [Space]
    [Header("Events on finish")]
    public UnityEvent onBlinkFinished;

    private void Start()    // !! DANGEROUS !! Button trigger and object trigger are not compatible
    {
        if(isButtonTrigger)
        {
            toObserve = gameObject;
        }
    }
}
