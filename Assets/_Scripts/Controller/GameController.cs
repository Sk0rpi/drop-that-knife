using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private Timer timer;
    public TMP_Text countdown;
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.SetTimer(30);
    }

    // Update is called once per frame
    void Update()
    {
        timer.updateTimer();
        if (timer.IsRunning())
        {
            countdown.text = "Timer: " + Mathf.Floor(timer.GetTimeRemaining());
        }
        else
        {
            countdown.text = "";
            this.ChangeScene(SceneController.Scene.scene_3);
        }
    }

    public void ChangeScene(SceneController.Scene scene)
    {
        SceneController.Load(scene);
    }
}
