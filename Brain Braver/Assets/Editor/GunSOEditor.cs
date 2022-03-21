using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GunSO))]
public class GunSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GunSO gun = (GunSO)target;

        if (GUILayout.Button("Reset Data"))
            gun.ResetGunData();
    }
}
