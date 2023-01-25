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
    [SerializeField] GameObject[] teleportationRayObjects;

    [Header("Turn")]
    [SerializeField] ContinuousTurnProviderBase continuousTurnProvider;

    [SerializeField] SnapTurnProviderBase snapTurnProvider;

    [Header("Change Defaults")]
    [SerializeField] static int moveType = 0;
    [SerializeField] static int turnType = 0;

    private void Awake()
    {
        ActivateMovement();
    }

    private void SetAllRaysActive(bool activate)
    {
        foreach(GameObject ray in teleportationRayObjects)
        {
            ray.SetActive(activate);
        }
    }

    void SetContinuousMove()
    {
        Debug.Log("Set ContinuousMove");
        teleportationProvider.enabled = false;
        activateTeleportationRay.enabled = false;
        SetAllRaysActive(false);

        continuousMoveProvider.enabled = true;

        moveType = 1;
    }

    void SetTeleportMove()
    {
        Debug.Log("Set SnapMove");
        continuousMoveProvider.enabled = false;

        teleportationProvider.enabled = true;
        activateTeleportationRay.enabled = true;
        SetAllRaysActive(true);

        moveType = 0;
    }

    void SetContinuousTurn()
    {
        Debug.Log("Set ContinuousTurn");
        snapTurnProvider.enabled = false;

        continuousTurnProvider.enabled = true;

        turnType = 1;
    }

    void SetSnapTurn()
    {
        Debug.Log("Set SnapTurn");
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
            
        }
        else
        {
            SetContinuousTurn();
        }
    }

    public void DeactivateMovement()
    {
        teleportationProvider.enabled = false;
        activateTeleportationRay.enabled = false;
        SetAllRaysActive(false);

        continuousMoveProvider.enabled = false;
    }

    public void ActivateMovement()
    {
        if (moveType == 0)
        {
            SetTeleportMove();
        }
        else
        {
            SetContinuousMove();
        }

        if (turnType == 0)
        {
            SetSnapTurn();
        }
        else
        {
            SetContinuousTurn();
        }
    }
}
