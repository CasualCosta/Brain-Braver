using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Data", menuName = "Scriptable Objects/Game Data SO")]
public class GameDataSO : ScriptableObject
{
    public PlayerSO player;
    public RoomSO[] rooms; 
    public GunSO[] guns; //What the hell is this for again?
    public CrossSceneSO crossScene;
    public int saveRoomIndex;



    public void ResetData()
    {
        crossScene.spawnPointIndex = 0;
        saveRoomIndex = 0;
        foreach(RoomSO r in rooms)
            r.ResetData();
        SaveSystem.DeleteProgress();
    }

    public void SetGameData(GameData gameData)
    {
        if (gameData == null)
            return;
        crossScene.spawnPointIndex = gameData.spawnPointIndex;
        saveRoomIndex = gameData.sceneIndex;
        //SetRoomSOs(gameData.rooms);
    }

    void SetRoomSOs(RoomData[] rds)
    {
        if(rooms.Length < 2)
        {
            Debug.LogWarning("Rooms not found.");
            return;
        }
        for(int i = 0; i < rooms.Length; i++)
        {
            int j = 0;
            for(j = 0; j < rooms[i].deletedEnemies.Length; j++)
                rooms[i].deletedEnemies[j].boolean = rds[i].deletedEnemies[j];
            for(j = 0; j < rooms[i].pickedPowerUps.Length; j++)
                rooms[i].pickedPowerUps[j].boolean = rds[i].pickedPowerUps[j];
            for(j = 0; j < rooms[i].storyEvents.Length; j++)
                rooms[i].storyEvents[j].index = rds[i].storyEvents[j];
        }
    }
}
