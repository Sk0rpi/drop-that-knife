using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RagdollActivate : MonoBehaviour
{
    [SerializeField] Animator ragdollAnimator;

    [SerializeField] GameObject ragdollRoot;
    [SerializeField] Rigidbody rootRb;

    [SerializeField] ParticleSystem bloodParticles;

    [SerializeField] Volume stabVolume;

    [SerializeField] TriggerValue triggerValueChanger;

    List<Collider> ragdollParts = new List<Collider>();


    private void Awake()
    {
        GetAllRagdollColliders();
        DeactivateRagdoll();
    }

    private void GetAllRagdollColliders()
    {
        Collider[] ragdollColliders = ragdollRoot.GetComponentsInChildren<Collider>();
        foreach (Collider collider in ragdollColliders)
        {
            if(collider.gameObject != this.gameObject)
                ragdollParts.Add(collider);
        }
    }
    private void DeactivateRagdoll()
    {
        foreach (Collider collider in ragdollParts)
        {
            collider.isTrigger = true;
            collider.attachedRigidbody.useGravity = false;
        }
    }

    private void ActivateRagdoll()
    {
        rootRb.velocity = Vector3.zero;
        rootRb.useGravity = false;
        
        ragdollAnimator.enabled = false;

        foreach (Collider collider in ragdollParts)
        {
            collider.isTrigger = false; 
            collider.attachedRigidbody.velocity = Vector3.zero;
            collider.attachedRigidbody.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = new ContactPoint[5];
        if (collision.GetContacts(contactPoints) < 1)
            return;

        foreach (ContactPoint contact in contactPoints)
        {
            if (contact.otherCollider == null)
                return;

            if (contact.otherCollider.CompareTag("Killer"))
            {
                // Found the right collision
                this.GetComponent<Collider>().enabled = false;

                bloodParticles.transform.position = contact.point;
                bloodParticles.transform.forward = contact.normal;
                bloodParticles.Play();

                if (GetComponent(typeof(TriggerValue)) != null)
                {
                    TriggerValue triggerValue = GetComponent(typeof(TriggerValue)) as TriggerValue;
                    triggerValue.triggered = true;
                }

                ActivateRagdoll();

                if(triggerValueChanger != null)
                    triggerValueChanger.triggered = true;

                if(stabVolume != null)
                    stabVolume.weight = 1;
            }
        }
    }
}
