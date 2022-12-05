using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectiveTrail : MonoBehaviour
{
    [SerializeField] NavMeshAgent player;

    [SerializeField] ParticleSystem trail;

    [SerializeField] float speed;

    [SerializeField] float trailFrequency;

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

    void CalculatePath()
    {
        Debug.Log(NavMesh.CalculatePath(player.transform.position, target.position, NavMesh.AllAreas, path));
    }

    private void Update()
    {
       
        MoveTrail();

    }

    void MoveTrail()
    {
        if (path.corners.Length > 0)    // If there is a path
        {

            if (currentCorner + 1 <= path.corners.Length - 1 && trail.transform.position != path.corners[currentCorner + 1])
            {
                Vector3 currentPosition = trail.transform.position;

                currentPosition = Vector3.MoveTowards(currentPosition, path.corners[currentCorner + 1], speed * Time.deltaTime);

                trail.transform.position = currentPosition;
            }
            else
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
