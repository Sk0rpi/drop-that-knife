using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using FMOD;
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

    bool dead;

    private void OnEnable()
    {
        gameController.onBlinkPerformed.AddListener(ChangeDeathVolume);
    }

    public void ChangeDeathVolume()
    {
        // If chanching to blink n6, add the new volume
        DOVirtual.Float(0, 1, 2f, ChangeGlobalVolumeWeight);
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

            stabParameterTrigger.TriggerParameters();

            // Particles
            ContactPoint contact = collision.contacts[0];
            bloodParticles.transform.position = contact.point;
            bloodParticles.transform.forward = contact.normal;
            bloodParticles.Play();
        }
    }

    private void ChangeTimeScale(float x)
    {
        Time.timeScale = x;
    }
}
