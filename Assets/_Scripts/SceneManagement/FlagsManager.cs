using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagsManager : MonoBehaviour
{
    public static FlagsManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
