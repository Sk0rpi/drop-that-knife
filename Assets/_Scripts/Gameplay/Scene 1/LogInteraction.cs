using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
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

    [SerializeField] Haptic hapticOnCarved;

    XRBaseController controller;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Killer"))
        {
            woodParticles.Play();
            if(!woodCarvedEmitter.IsPlaying())
                woodCarvedEmitter.Play();

            // Get controller of the hand holding the knife
            controller = collision.collider.GetComponent<XRBaseInteractable>().firstInteractorSelecting.transform.gameObject.GetComponent<XRBaseController>();
            if (controller != null)
            {
                StartCoroutine("WoodFeedback");
            }

            totem.position = transform.position;

            triggerValue.SetTriggered();

            Invoke("DestroyLog", 3f);
        }
    }

    private IEnumerator WoodFeedback()
    {
        yield return new WaitForSeconds(0.1f);

        hapticOnCarved.TriggerHaptic(controller);

        yield return new WaitForSeconds(0.5f);

        hapticOnCarved.TriggerHaptic(controller);

        yield return new WaitForSeconds(0.6f);

        hapticOnCarved.TriggerHaptic(controller);

        yield return new WaitForSeconds(0.4f);

        hapticOnCarved.TriggerHaptic(controller);
    }

    void DestroyLog()
    {
        gameObject.SetActive(false);
    }
}
