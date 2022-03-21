using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameDataSO))]
public class GameDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameDataSO gameData = (GameDataSO)target;

        if (GUILayout.Button("Reset Data"))
            gameData.ResetData();

        if (GUILayout.Button("Reset Player"))
            gameData.player.ResetData();
    }
}
