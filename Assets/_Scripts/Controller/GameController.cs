using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // TriggerCase Const's
    private const int NONE = 0;
    private const int TIME = 1;
    private const int OBJECT = 2;
    private const int BUTTONPRESS = 3;
    
    private List<GameObject> blinks = new List<GameObject>();
    private int _blinkCounter = 0;
    public Animator animator;
    public TMP_Text debug_Blink;
    public TMP_Text debug_Timer;
    private Timer _timer;
    private List<int> _triggerCases = new List<int>();
    private List<TriggerValue> _triggerValues = new List<TriggerValue>();
    private float _blinkDelay;

    public GameObject blinksparent;
    
    private void Start()
    {
        // Insert all children blinks in the blinks list
        foreach (Transform blink in blinksparent.transform)
        {
            if (blink.gameObject.name.Contains("BLINK"))
            {
                blinks.Add(blink.gameObject);
            }
        }
        
        // Activate the first blink and arm the trigger
        GameObject newBlink = blinks[_blinkCounter];
        newBlink.SetActive(true);
        Arm_Trigger(newBlink);
        
        Set_Debug_Blink();
        
        //animator.SetTrigger("Fade_in");
    }

    void Update()
    {
        Check_Trigger();
    }

    /// <summary>
    /// Arm_Trigger gets a blink gameObject and looks for the TriggerConnector of the gameObject.
    /// Based on which bool of the TriggerConnector is true, the Arm_Trigger function activates the matching trigger.
    /// </summary>
    /// <param name="newBlink"></param>
    private void Arm_Trigger(GameObject newBlink)
    {
        // Check if the blink got a TriggerConnector
        if (newBlink.GetComponent(typeof(TriggerConnector)) != null)
        {
            // If so, extract the TriggerConnector of the blink into a variable for further checks 
            TriggerConnector triggerConnector = newBlink.GetComponent(typeof(TriggerConnector)) as TriggerConnector;
            _blinkDelay = triggerConnector.blinkDelay;

            bool triggerActive = false;

            // Check if the trigger is timebased
            if (triggerConnector.isTimeTrigger)
            {
                // If so, set the TriggerCase to 1 (TIME) and set the Timer with the attached time in seconds
                _triggerCases.Add(TIME);
                Set_Timer(triggerConnector.timeInSec);
                _triggerValues.Add(null);
                triggerActive = true;
            }
            
            // Check if the trigger is lifebased
            if (triggerConnector.isObjectTrigger)
            {
                // If so, set the TriggerCase to 2 (OBJECT) and set the intern trigger GameObject
                _triggerCases.Add(OBJECT);
                Set_GameObject_Trigger(triggerConnector.toObserve);
                triggerActive = true;
            }
            
            if (triggerConnector.isButtonTrigger)
            {
                _triggerCases.Add(BUTTONPRESS);
                Set_GameObject_Trigger(triggerConnector.toObserve);
                triggerActive = true;
            }

            if (!triggerActive)
            {
                // If none of the trigger cases are fulfilled
                // Inform the developer that none trigger has been found
                _triggerCases.Add(NONE);
                Debug.LogError("NO BLINK-TRIGGER FOUND!");   
            }
        }

        // If no TriggerConnect has been found
        else
        {
            // Inform the developer that no TriggerConnector has been found
            Debug.LogError("NO TRIGGERCONNECTOR FOUND!");
        }

    }

    private void Set_GameObject_Trigger(GameObject gameObject)
    {
        if (gameObject.GetComponent(typeof(TriggerValue)) != null)
        {
            _triggerValues.Add(gameObject.GetComponent(typeof(TriggerValue)) as TriggerValue);
        }

        else
        {
            Debug.LogError("NO TRIGGER ATTACHED TO GAMEOBJECT!");
        }
    }

    /// <summary>
    /// Check_Trigger checks the status of the trigger based on the set triggerCase.
    /// It handles what happens when the trigger is activated and when the trigger is still deactivated.
    /// </summary>
    private void Check_Trigger()
    {
        for (int i = 0; i < _triggerCases.Count; i++)
        {
            int triggerCase = _triggerCases[i];
            Debug.Log("Checking case: " + triggerCase);
            TriggerValue triggerValue = _triggerValues[i];
            
            // Check if the triggerCase is the TIME trigger
            if (triggerCase == TIME)
            {
                Debug.Log("Confirm TIME");
                // If so, check if the timer is still running
                if (_timer.Is_Running())
                {
                    // If so, update the timer
                    _timer.Update_Timer();
                    Set_Debug_Timer();
                }

                // If the timer is done
                else
                {
                    Debug.Log("Timer ran out");
                    // Set the triggerCase to NONE
                    _triggerCases[i] = NONE;
                    // And call the blink change
                    StartCoroutine("Trigger_Blink_Change");
                }
            }
            
            // Check if the triggerCase is the OBJECT trigger
            else if (triggerCase == OBJECT)
            {
                Debug.Log("Confirm OBJECT");
                // If so, check if the gameObject is triggered
                if (triggerValue.triggered)
                {
                    // If so, set the triggerCase to NONE
                    _triggerCases[i] = NONE;
                    // And call the blink change
                    StartCoroutine("Trigger_Blink_Change");
                }
            }

            // Check if the triggerCase is the BUTTONPRESS trigger
            else if (triggerCase == BUTTONPRESS)
            {
                Debug.Log("Confirm BUTTONPRESS");
                // If so, check if the gameObject is triggered
                if (triggerValue.triggered)
                {
                    // If so, set the triggerCase to NONE
                    _triggerCases[i] = NONE;
                    // And call the blink change
                    StartCoroutine("Trigger_Blink_Change");
                }
            }
        }
    }

    public void Set_Timer(int timeInSec)
    {
        _timer = gameObject.AddComponent<Timer>();
        _timer.Set_Timer(timeInSec);
        Set_Debug_Timer();
    }

    private void Set_Debug_Blink()
    {
        debug_Blink.text = "Blink: " + (_blinkCounter + 1);
    }

    private void Set_Debug_Timer()
    {
        debug_Timer.text = "Timer: " + Mathf.Floor(_timer.Get_TimeRemaining());
    }

    /// <summary>
    /// Trigger_Blink_Change deactivates the current blink, activates the next one and triggers a blink animation
    /// </summary>
    public IEnumerator Trigger_Blink_Change()
    {
        _triggerCases.Clear();
        _triggerValues.Clear();
        Debug.Log("Changing Blink...");

        // Wait for a delay we can set up in each triggerConnector
        yield return new WaitForSecondsRealtime(_blinkDelay);

        // Trigger Blink-Animation Fade_out
        animator.SetTrigger("Fade_out");

        // Wait for a delay so everything shows up properly
        yield return new WaitForSecondsRealtime(1f);

        Debug.Log("Blink changed!");

        // Get old blink object
        GameObject oldBlink = blinks[_blinkCounter];
        // Deactivate old blink object
        oldBlink.SetActive(false);

        // Get new blink object
        _blinkCounter++;
        GameObject newBlink = blinks[_blinkCounter]; 
        // Trigger Blink-Animation Fade_in
        animator.SetTrigger("Fade_in");

        // Activate new blink object
        newBlink.SetActive(true);
        // Call the trigger arming with the new blink object
        Arm_Trigger(newBlink);

        Set_Debug_Blink();
    }

}
