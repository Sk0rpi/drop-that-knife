using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInteraction : MonoBehaviour
{
    [SerializeField]
    ParticleSystem woodParticles;
    [SerializeField]
    Transform totem;
    [SerializeField]
    TriggerValue triggerValue;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Killer"))
        {
            woodParticles.Play();

            totem.position = transform.position;

            triggerValue.SetTriggered();

            Invoke("DestroyLog", 4f);
        }
    }

    void DestroyLog()
    {
        gameObject.SetActive(false);
    }
}
