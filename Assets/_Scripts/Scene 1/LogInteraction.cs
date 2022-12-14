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

    [SerializeField]
    FMODUnity.StudioEventEmitter woodCarvedEmitter;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Killer"))
        {
            woodParticles.Play();
            if(!woodCarvedEmitter.IsPlaying())
                woodCarvedEmitter.Play();

            totem.position = transform.position;

            triggerValue.SetTriggered();

            Invoke("DestroyLog", 3f);
        }
    }

    void DestroyLog()
    {
        gameObject.SetActive(false);
    }
}
