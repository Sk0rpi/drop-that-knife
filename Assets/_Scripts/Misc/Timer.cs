using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Timer : MonoBehaviour
{
    private float _timeRemaining;
    private bool _timerIsRunning;

    public void SetTimer(float timeInSec)
    {
        _timeRemaining = timeInSec;
        _timerIsRunning = true;
    }

    public float GetTimeRemaining()
    {
        return _timeRemaining;
    }

    public bool IsRunning()
    {
        return _timerIsRunning;
    }

    public void updateTimer()
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
}
