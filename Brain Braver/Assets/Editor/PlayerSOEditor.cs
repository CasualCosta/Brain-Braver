using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerSO))]
public class PlayerSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerSO player = (PlayerSO)target;

        if (GUILayout.Button("Reset Data"))
            player.ResetData();
    }
}
