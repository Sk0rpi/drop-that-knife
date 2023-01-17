using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagtag : MonoBehaviour
{
    public bool flagStatus = false;
    
    public void Switch_Flag_Status()
    {
        flagStatus = !flagStatus;
    }
}
