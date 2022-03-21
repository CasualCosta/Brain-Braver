using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlideInputType { TapToSwap = 0, HoldToSlide = 1, HoldToBrake = 2 }
public enum DeathType { Casual = 0, Softcore = 1, Hardcore = 2 }
[CreateAssetMenu(menuName = "Scriptable Objects/Options SO", fileName = "New Options")]
public class OptionsSO : ScriptableObject
{
    public SlideInputType slideInput;
    public DeathType deathType;
    [Range(0,5)] public int frontalCameraOffset;
    [Range(1, 10)] public int cameraSpeed;
    [Range(1, 4)] public int cameraSize;
    [Range(0.1f, 1f)] public float cameraResizeSpeed;
    [Range(0, 1f)] public float dialogueTime;

    public void SetSlideType(int i) => slideInput = (SlideInputType)i;
    public void SetCameraOffset(float i) => frontalCameraOffset = (int)i;
    public void SetCameraSpeed(float i) => cameraSpeed = (int)i;
    public void SetCameraResizeSpeed(float f) => cameraResizeSpeed = f;
    public void SetDialogueSpeed(float f) => dialogueTime = f;
}
