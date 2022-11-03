using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // TriggerCase Const's
    private const int NONE = 0;
    private const int TIME = 1;
    private const int LIFE = 2;
    private const int MOVE = 3;
    
    public List<GameObject> blinks;
    private int _blinkCounter = 0;
    public Animator animator;
    public TMP_Text debug_Blink;
    public TMP_Text debug_Timer;
    private Timer _timer;
    private int _triggerCase = NONE;
    private Trigger _trigger;

    private void Start()
    {
        // Activate the first blink and arm the trigger
        GameObject newBlink = blinks[_blinkCounter];
        newBlink.SetActive(true);
        Arm_Trigger(newBlink);
        
        Set_Debug_Blink();
        
        animator.SetTrigger("Fade_in");;
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

            // Check if the trigger is timebased
            if (triggerConnector.isTimeTrigger)
            {
                // If so, set the TriggerCase to 1 (TIME) and set the Timer with the attached time in seconds
                _triggerCase = TIME;
                Set_Timer(triggerConnector.timeInSec);
            }
            
            // Check if the trigger is lifebased
            else if (triggerConnector.isAliveTrigger)
            {
                // If so, set the TriggerCase to 2 (LIFE) and set the intern trigger GameObject (rigbody)
                _triggerCase = LIFE;
                Set_GameObject_Trigger(triggerConnector.toObserve);
            }
            
            // Check if the trigger is movementbased
            else if (triggerConnector.isMovementTrigger)
            {
                // If so, set the TriggerCase to 3 (MOVE) and set the intern trigger GameObject (walkable Area)
                _triggerCase = MOVE;
                Set_GameObject_Trigger(triggerConnector.toObserve);
            }

            // If none of the trigger cases are fulfilled
            else
            {
                // Inform the developer that none trigger has been found
                _triggerCase = NONE;
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
        if (gameObject.GetComponent(typeof(Trigger)) != null)
        {
            _trigger = gameObject.GetComponent(typeof(Trigger)) as Trigger;
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
        // Check if the triggerCase is the TIME trigger
        if (_triggerCase == TIME)
        {
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
                // Set the triggerCase to NONE
                _triggerCase = NONE;
                // And call the blink change
                Trigger_Blink_Change();
            }
        }
        
        // Check if the triggerCase is the LIFE trigger
        else if (_triggerCase == LIFE)
        {
            // If so, check if the gameObject is triggered
            if (_trigger.triggered)
            {
                // If so, set the triggerCase to NONE
                _triggerCase = NONE;
                // And call the blink change
                Trigger_Blink_Change();
            }
        }
        
        // Check if the triggerCase is the MOVE trigger
        else if (_triggerCase == MOVE)
        {
            // If so, check if the gameObject is triggered
            if (_trigger.triggered)
            {
                // If so, set the triggerCase to NONE
                _triggerCase = NONE;
                // And call the blink change
                Trigger_Blink_Change();
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
    public void Trigger_Blink_Change()
    {
        // Trigger Blink-Animation Fade_out
        animator.SetTrigger("Fade_out");
        
        // Get old blink object
        GameObject oldBlink = blinks[_blinkCounter];
        // Deactivate old blink object
        oldBlink.SetActive(false);
        
        _blinkCounter++;
        
        // Get new blink object
        GameObject newBlink = blinks[_blinkCounter];
        // Activate new blink object
        newBlink.SetActive(true);
        // Call the trigger arming with the new blink object
        Arm_Trigger(newBlink);
        
        Set_Debug_Blink();
        
        // Trigger Blink-Animation Fade_in
        animator.SetTrigger("Fade_in");
    }
}
