using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomBool
{
    public bool boolean;
    [TextArea(1, 5)] public string description;
}
[System.Serializable]
public class RoomInt
{
    public int index;
    [TextArea(1, 5)] public string description;
}
[CreateAssetMenu(fileName = "New Room", menuName = "Scriptable Objects/Room SO")]
public class RoomSO : ScriptableObject
{
    public AudioClip soundtrack;

    //All the bools are false by standard
    public RoomBool[] pickedPowerUps;
    public RoomBool[] deletedEnemies; //for bosses and such
    public RoomInt[] storyEvents;
    [TextArea(1, 3)]
    public string loadMessage;

    public void ResetData()
    {
        if (pickedPowerUps.Length == 0 && deletedEnemies.Length == 0 && storyEvents.Length == 0)
            return;
        ResetRoomBools(pickedPowerUps);
        ResetRoomBools(deletedEnemies);
        ResetRoomInts(storyEvents);
    }
    void ResetRoomBools(RoomBool[] rbs)
    {
        if (rbs.Length < 1)
            return;
        foreach (RoomBool r in rbs)
            r.boolean = false;
    }
    void ResetRoomInts(RoomInt[] ris)
    {
        if (ris.Length < 1)
            return;
        foreach (RoomInt r in ris)
            r.index = 0;
    }
}
