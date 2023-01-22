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
        else if (this != instance)
        {
            UpdateCurrentSceneFlags();
            Destroy(instance);
            instance = this;
        }
    }

    private void UpdateCurrentSceneFlags()
    {
        Flagtag[] currentFlagTags = GetComponentsInChildren<Flagtag>(true);
        Flagtag[] previousFlagTags = instance.GetComponentsInChildren<Flagtag>(true);

        for(int i = 0; i < currentFlagTags.Length; i++)
        {
            currentFlagTags[i].flagStatus = previousFlagTags[i].flagStatus;
        }
    }

}
