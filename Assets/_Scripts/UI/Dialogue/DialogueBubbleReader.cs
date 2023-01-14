using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DialogueBubbleReader : MonoBehaviour
{
    public DialogueBubbleScriptableObject bubbleText;

    public Image shadow;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = bubbleText.text_nl;
        shadow.color = bubbleText.shadowColor;

        LocalizationSettings.SelectedLocaleChanged += LocalizeTextBubble;
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= LocalizeTextBubble;
    }

    protected virtual void LocalizeTextBubble(Locale locale)
    {

        if (locale.Identifier.Code == "en-GB")
            text.text = bubbleText.text_eng;
        else
            text.text = bubbleText.text_nl;
    }
}
