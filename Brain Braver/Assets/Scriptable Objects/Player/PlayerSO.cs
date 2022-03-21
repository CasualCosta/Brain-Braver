using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwitchDrag { Drag = 0, Slide = 1 }
public enum OrbiterAim { Cursor = 0, Player = 1 }
[CreateAssetMenu(menuName = "Scriptable Objects/Player SO", fileName = "New Player SO")]
public class PlayerSO : ScriptableObject
{
    public PlayerSO defaultData;
    public GameDataSO gameData;
    public Vector2Int Experience = new Vector2Int(0, 1000);
    public int level = 1;
    public int orbiters = 0;
    public int followers = 0;
    public Vector2 Health = new Vector2(100, 100);
    public Vector3 Drag = new Vector3(0.3f, 1f, 2f);
    public SwitchDrag switchDrag;
    public OrbiterAim orbiterAim;
    public float moveForce = 100f;

    public List<GunSO> Guns;
    public GunSO equippedGun;
    public int weaponIndex;

    public void ResetData()
    {
        Experience = defaultData.Experience;
        level = defaultData.level;
        Health = defaultData.Health;
        Drag = defaultData.Drag;
        moveForce = defaultData.moveForce;
        foreach (GunSO gun in Guns)
            if(gun)
                gun.ResetGunData();
        Guns = defaultData.Guns;
        equippedGun = defaultData.equippedGun;
        weaponIndex = defaultData.weaponIndex;
        orbiters = defaultData.orbiters;
        followers = defaultData.followers;
    }

    //What is this for? Load game? -Yup
    public void SetPlayerData(GameData gameData)
    {
        if (gameData == null)
            return;
        Health.x = Health.y = gameData.health;
        Drag.y = gameData.brake;
        Drag.x = gameData.slide;
        level = gameData.level;
        Experience.x = gameData.experience;
        weaponIndex = gameData.weaponIndex;
        Guns = SetGuns (gameData.gunIndexes);
        equippedGun = Guns[weaponIndex];
    }
    List<GunSO> SetGuns(int[] gunIndexes)
    {
        List<GunSO> guns = new List<GunSO>();
        for(int i = 0; i < gunIndexes.Length; i++)
        {
            GunSO g = gunIndexes[i] >= 0 ? gameData.guns[i] : null;
            guns.Add(g);
        }
        return guns;
    }
}
