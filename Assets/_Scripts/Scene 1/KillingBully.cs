using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
public class KillingBully : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameController gameController;
    [SerializeField]
    Volume deathVolume;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killer") && !dead)
        {
            Debug.Log("Dead");
            dead = true;
            animator.SetTrigger("Death");
            DOVirtual.Float(1, 0.5f, 2f, ChangeTimeScale);
        }
        
    }

    private void ChangeTimeScale(float x)
    {
        Time.timeScale = x;
    }
}
