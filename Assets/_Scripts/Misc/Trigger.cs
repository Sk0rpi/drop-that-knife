using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    // TriggerCase Const's
    private const int NONE = 0;
    private const int TIME = 1;
    private const int OBJECT = 2;
    private const int BUTTONPRESS = 3;
    
    private List<int> _triggerCases = new List<int>();
    private List<TriggerValue> _triggerValues = new List<TriggerValue>();
    public float blinkDelay;
    private Timer _timer;

    public TriggerConnector triggerConnector;
    public TMP_Text debug_Timer;

    public bool Check_Trigger()
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
                    return true;
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
                    return true;
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
                    return true;
                }
            }
        }
        return false;
    }
    
    public void Arm_Trigger()
    {
        this.debug_Timer = debug_Timer;
        blinkDelay = triggerConnector.blinkDelay;

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
    
    public void Set_Timer(int timeInSec)
    {
        _timer = gameObject.AddComponent<Timer>();
        _timer.Set_Timer(timeInSec);
        Set_Debug_Timer();
    }

    private void Set_Debug_Timer()
    {
        debug_Timer.text = "Timer: " + Mathf.Floor(_timer.Get_TimeRemaining());
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
}
