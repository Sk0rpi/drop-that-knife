using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/DialogueBubbleScriptableObject", order = 1)]
public class DialogueBubbleScriptableObject : ScriptableObject
{
    [TextArea(3, 6)]
    public string text_eng;
    [TextArea(3, 6)]
    public string text_nl;
    [TextArea(3, 6)]
    public string text_ger;
    [TextArea(3, 6)]
    public string text_esp;
    public Color shadowColor;
}
