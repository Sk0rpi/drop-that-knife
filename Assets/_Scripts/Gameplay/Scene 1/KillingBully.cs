using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using FMOD;
using UnityEngine.XR.Interaction.Toolkit;
public class KillingBully : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameController gameController;
    [SerializeField]
    Volume deathVolume;
    [SerializeField]
    ParticleSystem bloodParticles;
    [SerializeField]
    FMODUnity.StudioParameterTrigger stabParameterTrigger;

    [SerializeField]
    Haptic hapticOnBullyKilled;

    bool dead;

    public int numberOfStabs = 0;

    public void ChangeDeathVolume()
    {
        // If chanching to blink n6, add the new volume
        DOVirtual.Float(0, 1, 4f, ChangeGlobalVolumeWeight);
    }

    public void ChangeGlobalVolumeWeight(float x)
    {
        deathVolume.weight = x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Killer") && !dead)
        {
            dead = true;
            animator.SetTrigger("Death");
            DOVirtual.Float(1, 0.5f, 2f, ChangeTimeScale);

            // Search for controller in knife hand
            XRBaseController controller = collision.collider.GetComponent<XRBaseInteractable>().firstInteractorSelecting.transform.gameObject.GetComponent<XRBaseController>();
            hapticOnBullyKilled.TriggerHaptic(controller);

            stabParameterTrigger.TriggerParameters();

            // Particles
            ContactPoint contact = collision.contacts[0];
            bloodParticles.transform.position = contact.point;
            bloodParticles.transform.forward = contact.normal;
            bloodParticles.Play();

            Invoke("ChangeScene", 5f);
        }
        else if(dead)
        {
            numberOfStabs++;
        }
    }

    private void ChangeTimeScale(float x)
    {
        Time.timeScale = x;
    }

    private void ChangeScene()
    {
        Time.timeScale = 1;
        SceneJump.instance.ChangeScene(2);
    }
}
