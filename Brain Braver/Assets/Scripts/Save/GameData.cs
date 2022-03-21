using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    //Player related data
    public float health;
    public float brake;
    public float slide;
    public int level;
    public int experience;
    public int weaponIndex;
    public int[] gunIndexes;
    public int orbiters;
    public OrbiterAim orbiterAim;


    //Game Data (AKA fuck my life. How am I supposed to even get this shit working?)
    public int sceneIndex;
    public int spawnPointIndex;
    public RoomData[] rooms; //now here is where the fun begins

    public GameData(GameDataSO gameData, PlayerSO playerData)
    {
        health = playerData.Health.y;
        brake = playerData.Drag.z;
        slide = playerData.Drag.x;
        level = playerData.level;
        experience = playerData.Experience.x;
        weaponIndex = playerData.weaponIndex;
        orbiters = playerData.orbiters;
        orbiterAim = playerData.orbiterAim;

        spawnPointIndex = gameData.crossScene.spawnPointIndex;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        rooms = ConvertRooms(gameData.rooms);
        gunIndexes = GetGunIndexes(playerData.Guns, gameData.guns);
    }

    RoomData[] ConvertRooms(RoomSO[] roomsSO)
    {
        if (roomsSO.Length < 2)
        {
            Debug.LogWarning("No rooms found.");
            return null;
        }
        RoomData[] returnable = new RoomData[roomsSO.Length];
        for(int i = 0; i < roomsSO.Length; i++)
            returnable[i] = new RoomData(roomsSO[i]);
        return returnable;
    }

    int[] GetGunIndexes(List<GunSO> playerGuns, GunSO[] allGuns)
    {
        int[] indexes = new int[playerGuns.Count];
        for (int i = 0; i < playerGuns.Count; i++)
            indexes[i] = Array.IndexOf(allGuns, playerGuns[i]);
        foreach (int i in indexes)
            Console.Write(i + " ");
        return indexes;
    }
}
