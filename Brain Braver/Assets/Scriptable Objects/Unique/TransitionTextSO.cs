using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Transition Text", menuName = "Scriptable Objects/Transition Text SO")]
public class TransitionTextSO : ScriptableObject
{
    [Tooltip("List of generic text for the screen loading.")]
    [TextArea(1, 3)] public string[] messages;
    [HideInInspector] public string currentMessage = "Welcome to Brain Braver";
}
