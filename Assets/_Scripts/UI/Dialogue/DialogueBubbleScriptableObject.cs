using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueText", menuName = "ScriptableObjects/DialogueBubbleScriptableObject", order = 1)]
public class DialogueBubbleScriptableObject : ScriptableObject
{
    [TextArea(10, 100)]
    public string text;
    public Color shadowColor;
}
