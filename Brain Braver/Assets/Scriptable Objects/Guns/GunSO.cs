using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType { Blaster = 0, Burner = 1, Rail = 2 }
public enum FireType { Semi = 0, Auto = 1 }
public enum SpreadType { Single = 0, Double = 1, Triple = 2, Diagonal = 3 } //Diagonal is off for now.
[System.Serializable]

[CreateAssetMenu(fileName = "New Gun", menuName = "Scriptable Objects/Gun SO")]
public class GunSO : ScriptableObject
{
    public GunSO defaultGun;
    //public GunType gunType;
    public FireType fireType;
    public SpreadType spreadType;
    public GameObject bulletPrefab;
    public bool isInfinite;
    public Vector2Int Ammo;
    public int recoversFromPickUp;
    public float fireRate;

    [Header("Bullet Data")]
    public float damage;
    public float bulletForce;
    public float bulletLifeTime;

    public void ResetGunData()
    {
        Ammo.x = Ammo.y;
        /*
        Ammo = defaultGun.Ammo;
        recoversFromPickUp = defaultGun.recoversFromPickUp;
        fireRate = defaultGun.fireRate;
        damage = defaultGun.damage;
        */
    }

    public bool CheckIfUpgradeable() => (int)spreadType < 2 ? true : false;

}
