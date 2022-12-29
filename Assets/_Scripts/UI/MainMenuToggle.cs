using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuToggle : MonoBehaviour
{
    [SerializeField] float transitionDuration = 1f;

    [SerializeField] CanvasGroup mainButtons;
    [SerializeField] CanvasGroup settingButtons;

    public void SwitchToSettings()
    {
        ActivateSettings();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(mainButtons.DOFade(0, transitionDuration).OnComplete(DeactivateMain));
        sequence.Insert(transitionDuration * 0.5f, settingButtons.DOFade(1, transitionDuration));
    }

    public void SwitchToMain()
    {
        ActivateMain();

        Sequence sequence = DOTween.Sequence();

        sequence.Append(settingButtons.DOFade(0, transitionDuration).OnComplete(DeactivateSettings));
        sequence.Insert(transitionDuration * 0.5f, mainButtons.DOFade(1, transitionDuration));
    }

    void DeactivateMain()
    {
        mainButtons.gameObject.SetActive(false);
    }
    void ActivateMain()
    {
        mainButtons.gameObject.SetActive(true);
    }

    void DeactivateSettings()
    {
        settingButtons.gameObject.SetActive(false);
    }
    void ActivateSettings()
    {
        settingButtons.gameObject.SetActive(true);
    }
}
