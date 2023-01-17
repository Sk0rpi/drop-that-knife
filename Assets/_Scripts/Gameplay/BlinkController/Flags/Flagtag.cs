using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagtag : MonoBehaviour
{
    public bool flagStatus = false;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void Switch_Flag_Status()
    {
        flagStatus = !flagStatus;
    }
}
