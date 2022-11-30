using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltInventoryFollowHead : MonoBehaviour
{
    [SerializeField]
    Transform headTransform;
    [SerializeField]
    float yPosition = 0.8799999f;
    [SerializeField]
    float damping = 2f;


    // Update is called once per frame
    void Update()
    {
        // Set Position
        Vector3 position = headTransform.position;
        position.y = yPosition;
        transform.position = position;

        // Set Rotation
        Vector3 headVector = headTransform.forward;
        headVector.y = 0;
        Vector3 inventoryVector = transform.forward;
        inventoryVector.y = 0;

        float angle = Vector3.Angle(headVector, inventoryVector);
        if(Vector3.Cross(headVector, inventoryVector).y > 0)    // Get the negative value of the angle
            angle = -angle;


        Debug.Log(angle);
        if (angle > 20 || angle < -20)
        {
            Quaternion rotation = Quaternion.Euler(0, angle, 0);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * damping);

        }
       
    }
}
