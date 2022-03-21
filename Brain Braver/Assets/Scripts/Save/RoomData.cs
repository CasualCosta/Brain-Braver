using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    public bool[] pickedPowerUps;
    public bool[] deletedEnemies;
    public int[] storyEvents;
    public RoomData(RoomSO sO)
    {
        pickedPowerUps = GetRoomBools(sO.pickedPowerUps);
        deletedEnemies = GetRoomBools(sO.deletedEnemies);
        storyEvents = GetRoomInts(sO.storyEvents);
    }

    bool[] GetRoomBools(RoomBool[] source)
    {
        bool[] target = new bool[source.Length];
        for (int i = 0; i < source.Length; i++)
            target[i] = source[i].boolean;
        return target;
    }
    int[] GetRoomInts(RoomInt[] source)
    {
        int[] target = new int[source.Length];
        for (int i = 0; i < source.Length; i++)
            target[i] = source[i].index;
        return target;
    }
}
