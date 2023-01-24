using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveObjectToPoint : MonoBehaviour
{

    public Transform objective;
    public Transform movementTarget;

    public float speed = 10f;

    public UnityEvent onObjectiveReached;

    public void OnEnable()
    {
        StartCoroutine("RunLineFunction"); 
        Debug.Log("Start MovingPlayer...");
    }

    IEnumerator RunLineFunction()
    {
        // Only keep going if the player is not there
        while (!TestPositions())
        {
            yield return new WaitForEndOfFrame();
            movementTarget.position = new Vector3(movementTarget.position.x, movementTarget.position.y, movementTarget.position.z + speed * Time.deltaTime);
        }
        onObjectiveReached.Invoke();
    }

    private bool TestPositions()
    {
        return Vector3.Distance(movementTarget.position, objective.position) <= 1;
    }
}
