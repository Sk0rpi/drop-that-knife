using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDecider : MonoBehaviour
{
    [Header("Which flag should be checked?")]
    public Flagtag flag;
    
    [Space]
    [Header("Where does the trigger leads to?")]
    public GameObject trueWay;
    public GameObject falseWay;
    
}
