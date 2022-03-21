using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[CustomEditor(typeof(RoomSO))]
public class RoomSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomSO room = (RoomSO)target;

        if (GUILayout.Button("Reset Data"))
            room.ResetData();
    }
}
