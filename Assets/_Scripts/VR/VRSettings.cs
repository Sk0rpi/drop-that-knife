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
    [SerializeField] GameObject teleportationRayObject;

    [Header("Turn")]
    [SerializeField] ContinuousTurnProviderBase continuousTurnProvider;

    [SerializeField] SnapTurnProviderBase snapTurnProvider;

    [Header("Change Defaults")]
    [SerializeField] static int moveType = 1;
    [SerializeField] static int turnType = 0;

    private void Awake()
    {
        if(moveType == 0)
        {
            SetTeleportMove();
        }
        else
        {
            SetContinuousMove();
        }

        if(turnType == 0)
        {
            SetSnapTurn();
        }
        else
        {
            SetContinuousTurn();
        }
    }

    void SetContinuousMove()
    {
        teleportationProvider.enabled = false;
        activateTeleportationRay.enabled = false;
        teleportationRayObject.SetActive(false);

        continuousMoveProvider.enabled = true;

        moveType = 1;
    }

    void SetTeleportMove()
    {
        continuousMoveProvider.enabled = false;

        teleportationProvider.enabled = true;
        activateTeleportationRay.enabled = true;
        teleportationRayObject.SetActive(true);

        moveType = 0;
    }

    void SetContinuousTurn()
    {
        snapTurnProvider.enabled = false;

        continuousTurnProvider.enabled = true;

        turnType = 1;
    }

    void SetSnapTurn()
    {
        continuousTurnProvider.enabled = false;

        snapTurnProvider.enabled = true;

        turnType = 0;
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
