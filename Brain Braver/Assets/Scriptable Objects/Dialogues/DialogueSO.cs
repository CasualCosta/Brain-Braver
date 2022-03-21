using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Speaker{ Stone, Brain }

[System.Serializable]
public class Talker
{

}


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue SO")]
public class DialogueSO : ScriptableObject
{
    [SerializeField] Speaker speaker;
    [TextArea(2, 5)]
    public string[] sentences;

    public string GetSpeaker()
    {
        switch(speaker)
        {
            case Speaker.Stone: return "Dr. Stone";
            case Speaker.Brain: return "Brain";
            default: return "";
        }
    }
}
