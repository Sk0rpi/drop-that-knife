using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[ExecuteAlways]
public class DialogueBubbleReader : MonoBehaviour
{
    public DialogueBubbleScriptableObject bubbleText;

    public Image shadow;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        UpdateBubble();
    }

    /*private void OnValidate()
    {
        // In editor update the bubble as needed
        UpdateBubble();
    }*/

    private void UpdateBubble()
    {
        if (bubbleText != null)
        {
            LocalizeTextBubble(LocalizationSettings.SelectedLocale);
            shadow.color = bubbleText.shadowColor;

            LocalizationSettings.SelectedLocaleChanged += LocalizeTextBubble;
        }
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
