using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timeRemaining;
    private bool _timerIsRunning;

    public void Set_Timer(float timeInSec)
    {
        _timeRemaining = timeInSec;
        _timerIsRunning = true;
    }
    
    public void Update_Timer()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _timeRemaining = 0;
                _timerIsRunning = false;
            }
        }
    }

    public float Get_TimeRemaining()
    {
        return _timeRemaining;
    }

    public bool Is_Running()
    {
        return _timerIsRunning;
    }
}
