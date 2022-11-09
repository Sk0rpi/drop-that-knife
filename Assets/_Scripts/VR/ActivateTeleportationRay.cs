using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] GameObject rightTeleportationRay;

    [SerializeField] InputActionProperty rightActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rightTeleportationRay.SetActive(rightActivate.action.ReadValue<Vector2>().y > 0.1f);
    }
}
