using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    // TriggerCase Const's
    private const int NONE = 0;
    private const int TIME = 1;
    private const int OBJECT = 2;
    private const int BUTTONPRESS = 3;

    // Blink attributes
    private List<GameObject> blinks = new List<GameObject>();
    private int _blinkCounter = 0;

    // Public references
    public Animator animator;
    public TMP_Text debug_Blink;
    public TMP_Text debug_Timer;

    // Members
    private Timer _timer;
    private int _triggerCase = NONE;
    private TriggerValue _triggerValue;
    private TriggerConnector _triggerConnector;
    private GameObject player;
    public GameObject blinksParent;
    public UnityEvent onBlinkPerformed;
    
    private void Start()
    {
        onBlinkPerformed = new UnityEvent();
        // Insert all children blinks in the blinks list
        foreach (Transform blink in blinksParent.transform)
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

        player = GameObject.FindGameObjectWithTag("Player");
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
            _triggerConnector = newBlink.GetComponent(typeof(TriggerConnector)) as TriggerConnector;

            // Check if the trigger is timebased
            if (_triggerConnector.isTimeTrigger)
            {
                // If so, set the TriggerCase to 1 (TIME) and set the Timer with the attached time in seconds
                _triggerCase = TIME;
                Set_Timer(_triggerConnector.timeInSec);
            }
            
            // Check if the trigger is lifebased
            else if (_triggerConnector.isObjectTrigger)
            {
                // If so, set the TriggerCase to 2 (OBJECT) and set the intern trigger GameObject
                _triggerCase = OBJECT;
                Set_GameObject_Trigger(_triggerConnector.toObserve);
            }
            
            else if (_triggerConnector.isButtonTrigger)
            {
                _triggerCase = BUTTONPRESS;
                Set_GameObject_Trigger(_triggerConnector.toObserve);
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
        if (gameObject.GetComponent(typeof(TriggerValue)) != null)
        {
            _triggerValue = gameObject.GetComponent(typeof(TriggerValue)) as TriggerValue;
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
                Debug.Log("Timer ran out");
                // Set the triggerCase to NONE
                _triggerCase = NONE;
                // And call the blink change
                StartCoroutine("Trigger_Blink_Change");
            }
        }
        
        // Check if the triggerCase is the OBJECT trigger
        else if (_triggerCase == OBJECT)
        {
            // If so, check if the gameObject is triggered
            if (_triggerValue.triggered)
            {
                // If so, set the triggerCase to NONE
                _triggerCase = NONE;
                // And call the blink change
                StartCoroutine("Trigger_Blink_Change");
            }
        }

        // Check if the triggerCase is the BUTTONPRESS trigger
        else if (_triggerCase == BUTTONPRESS)
        {
            // If so, check if the gameObject is triggered
            if (_triggerValue.triggered)
            {
                // If so, set the triggerCase to NONE
                _triggerCase = NONE;
                // And call the blink change
                StartCoroutine("Trigger_Blink_Change");
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
        Debug.Log("Changing Blink...");

        // Wait for a delay we can set up in each triggerConnector
        yield return new WaitForSecondsRealtime(_triggerConnector.blinkDelay);

        animator.SetTrigger("Fade_out");

        // Wait for a delay so everything shows up properly
        yield return new WaitForSecondsRealtime(1f);

        Debug.Log("Blink changed, current: " + (_blinkCounter + 1));

        // Update player position if needed
        if (_triggerConnector.movePlayer)
        {
            player.transform.position = _triggerConnector.newPosition.position;
            player.transform.rotation = _triggerConnector.newPosition.rotation;
        }

        // Deactivate old blink object
        GameObject oldBlink = blinks[_blinkCounter];
        oldBlink.SetActive(false);

        // Get new blink object
        _blinkCounter++;
        GameObject newBlink = blinks[_blinkCounter];

        // Callbacks for blinks
        onBlinkPerformed.Invoke();     // +1 to get the same number as in editor


        animator.SetTrigger("Fade_in");

        // Call the trigger arming with the new blink object
        newBlink.SetActive(true);
        Arm_Trigger(newBlink);

        

        Set_Debug_Blink();
    }

}
