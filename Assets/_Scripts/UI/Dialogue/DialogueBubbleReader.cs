using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueBubbleReader : MonoBehaviour
{
    public DialogueBubbleScriptableObject bubbleText;

    public Image shadow;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = bubbleText.text;
        shadow.color = bubbleText.shadowColor;
    }
}
