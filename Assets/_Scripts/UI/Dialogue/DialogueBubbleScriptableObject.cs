using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/DialogueBubbleScriptableObject", order = 1)]
public class DialogueBubbleScriptableObject : ScriptableObject
{
    [TextArea(10, 10)]
    public string text_eng;
    [TextArea(10, 10)]
    public string text_nl;
    public Color shadowColor;
}
