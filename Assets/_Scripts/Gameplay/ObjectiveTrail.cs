using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectiveTrail : MonoBehaviour
{
    [SerializeField] NavMeshAgent player;

    [SerializeField] ParticleSystem trail;

    [SerializeField] float speed;

    [SerializeField] float trailPeriod;

    public Transform target;

    private NavMeshPath path;

    private bool pathChanged = false;
    private int currentCorner = 0;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        trail.transform.position = player.transform.position;
        path = new NavMeshPath();
        InvokeRepeating("CalculatePath", 0f, 2f);
    }

    // This is called every 2 seconds, in order to update the path.
    void CalculatePath()
    {
        if (target != null)
        {
            NavMesh.CalculatePath(player.transform.position, target.position, NavMesh.AllAreas, path);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        target = GameController.instance.trailTarget;
        if (timer > trailPeriod && target != null)
        {
            MoveTrail();
        }
        else if (target == null)
        {
            trail.Stop();
            trail.transform.position = player.transform.position;
        }
        
    }

    /// <summary>
    /// Move the trail when there is a path avaliable. 
    /// </summary>
    void MoveTrail()
    {
        if (path.corners.Length > 0)    // If there is a path
        {
            // The position of the next corner is not reached
            if (currentCorner + 1 <= path.corners.Length - 1 && trail.transform.position != path.corners[currentCorner + 1])
            {
                Vector3 currentPosition = trail.transform.position;

                currentPosition = Vector3.MoveTowards(currentPosition, path.corners[currentCorner + 1], speed * Time.deltaTime);

                trail.transform.position = currentPosition;
            }
            else // Position of the next corner reached
            {
                currentCorner++;
                if (currentCorner + 1 > path.corners.Length - 1) // End of the path
                {
                    trail.Stop();
                    trail.transform.position = player.transform.position;
                    trail.Play();
                    currentCorner = 0;
                    timer = 0;
                }
            }
        }
    }
  
}
