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
    float speed = 2f;
    [SerializeField]
    float maxDistanceAngle = 20f;


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

        if (angle > maxDistanceAngle)  // If the angle surpasses the maxDistance, rotate towards it
        {
            if (Vector3.Cross(headVector, inventoryVector).y > 0)    // In case of a negative value of the angle
                angle = -angle;

            Quaternion rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + angle, 0);

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotation, Time.deltaTime * speed);
        }
    }

    public static Quaternion ShortestRotation(Quaternion a, Quaternion b)

    {

        if (Quaternion.Dot(a, b) < 0)

        {

            return a * Quaternion.Inverse(Multiply(b, -1));

        }

        else return a * Quaternion.Inverse(b);

    }



    public static Quaternion Multiply(Quaternion input, float scalar)

    {
        return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
    }
}
