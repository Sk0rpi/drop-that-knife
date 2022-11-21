using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public Animator animator;
    public TMP_Text debug_Blink;
    public TMP_Text debug_Timer;

    public GameObject activeBlink;

    private Trigger _activeTrigger;

    private GameObject player;
    public UnityEvent onBlinkPerformed;

    private Dictionary<Trigger, GameObject> _triggerBlinkRelation = new Dictionary<Trigger, GameObject>();

    private void Start()
    {
        onBlinkPerformed = new UnityEvent();
        
        // Activate the first blink and arm the trigger
        activeBlink.SetActive(true);
        Arm_Triggers(activeBlink);
        
        Set_Debug_Blink();
        animator.SetTrigger("Fade_in");

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Check_Triggers();
    }

    /// <summary>
    /// Arm_Trigger gets a blink gameObject and looks for the TriggerConnector of the gameObject.
    /// Based on which bool of the TriggerConnector is true, the Arm_Trigger function activates the matching trigger.
    /// </summary>
    /// <param name="newBlink"></param>
    private void Arm_Triggers(GameObject newBlink)
    {
        // Check if the blink got a TriggerConnector
        if (newBlink.GetComponents(typeof(TriggerConnector)) != null)
        {
            // If so, extract the TriggerConnector of the blink into a variable for further checks 
            Component[] triggerConnectors = newBlink.GetComponents(typeof(TriggerConnector));
            foreach (TriggerConnector triggerConnector in triggerConnectors)
            {
                Trigger trigger = newBlink.AddComponent<Trigger>();
                trigger.debug_Timer = debug_Timer;
                trigger.triggerConnector = triggerConnector;
                
                trigger.Arm_Trigger();
                
                _triggerBlinkRelation.Add(trigger, triggerConnector.nextBlink);
            }
        }

        // If no TriggerConnect has been found
        else
        {
            // Inform the developer that no TriggerConnector has been found
            Debug.LogError("NO TRIGGERCONNECTOR FOUND!");
        }

    }

    /// <summary>
    /// Check_Trigger checks the status of the trigger based on the set triggerCase.
    /// It handles what happens when the trigger is activated and when the trigger is still deactivated.
    /// </summary>
    private void Check_Triggers()
    {
        bool triggered = false;
        foreach (KeyValuePair<Trigger, GameObject> entry in _triggerBlinkRelation)
        {
            if (entry.Key.Check_Trigger())
            {
                _activeTrigger = entry.Key;
                triggered = true;
                break;
            }
        }

        if (triggered)
        {
            StartCoroutine("Trigger_Blink_Change");
        }
        
    }

    private void Set_Debug_Blink()
    {
        debug_Blink.text = "Blink: " + (activeBlink.name);
    }


    /// <summary>
    /// Trigger_Blink_Change deactivates the current blink, activates the next one and triggers a blink animation
    /// </summary>
    public IEnumerator Trigger_Blink_Change()
    {
        Trigger trigger = _activeTrigger;
        GameObject nextBlink = _triggerBlinkRelation[trigger];
        Debug.Log("Changing Blink...");

        // Wait for a delay we can set up in each triggerConnector
        yield return new WaitForSecondsRealtime(trigger.blinkDelay);

        animator.SetTrigger("Fade_out");

        // Wait for a delay so everything shows up properly
        yield return new WaitForSecondsRealtime(1f);

        Debug.Log("Blink changed!");
        
        // Deactivate old blink object
        activeBlink.SetActive(false);
        
        if (trigger.triggerConnector.movePlayer)
        {
            player.transform.position = trigger.triggerConnector.newPosition.position;
            player.transform.rotation = trigger.triggerConnector.newPosition.rotation;
        }

        activeBlink = nextBlink;
        // Trigger Blink-Animation Fade_in
        animator.SetTrigger("Fade_in");

        // Activate new blink object
        activeBlink.SetActive(true);
        
        // Callbacks for blinks
        onBlinkPerformed.Invoke();     // +1 to get the same number as in editor
        
        // Call the trigger arming with the new blink object
        Arm_Triggers(activeBlink);        

        Set_Debug_Blink();
    }

}
