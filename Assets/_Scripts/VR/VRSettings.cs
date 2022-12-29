using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRSettings : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] ContinuousMoveProviderBase continuousMoveProvider;

    [SerializeField] TeleportationProvider teleportationProvider;
    [SerializeField] ActivateTeleportationRay activateTeleportationRay;

    [Header("Turn")]
    [SerializeField] ContinuousTurnProviderBase continuousTurnProvider;

    [SerializeField] SnapTurnProviderBase snapTurnProvider;

    
    void SetContinuousMove()
    {
        teleportationProvider.enabled = false;
        activateTeleportationRay.enabled = false;

        continuousMoveProvider.enabled = true;
    }

    void SetTeleportMove()
    {
        continuousMoveProvider.enabled = false;

        teleportationProvider.enabled = true;
        activateTeleportationRay.enabled = true;
    }

    void SetContinuousTurn()
    {
        snapTurnProvider.enabled = false;

        continuousTurnProvider.enabled = true;
    }

    void SetSnapTurn()
    {
        continuousTurnProvider.enabled = false;

        snapTurnProvider.enabled = true;
    }
    
    public void SetMove(int index)
    {
        if(index == 0)
        {
            SetTeleportMove();
        }
        else
        {
            SetContinuousMove();
        }
    }

    public void SetTurn(int index)
    {
        if(index == 0)
        {
            SetSnapTurn();
            Debug.Log("Set SnapTurn");
        }
        else
        {
            SetContinuousTurn();
            Debug.Log("Set ContinuousTurn");
        }
    }
}
