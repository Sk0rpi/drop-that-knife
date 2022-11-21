using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBubbleReader : MonoBehaviour
{
    public DialogueBubbleScriptableObject bubbleText;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = bubbleText.text;
    }
}
