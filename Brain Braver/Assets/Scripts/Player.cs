using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    #region VARIABLES
    public static Player Instance { get; private set; }
    [SerializeField] PlayerSO scriptableObject = null;
    [SerializeField] GameDataSO gameData = null;
    [SerializeField] OptionsSO options = null;
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] TrailRenderer trailRenderer = null;
    [SerializeField] Transform mainFirepoint = null;
    [SerializeField] Transform[] sideFirePoints = new Transform[2];
    [SerializeField] Transform[] diagonalFirePoints = new Transform[2];
    [SerializeField] SpriteRenderer engine = null;
    [SerializeField] Color coldEngineColor = Color.blue, hotEngineColor = Color.red;
    [SerializeField] KeyCode orboterAim = KeyCode.R;

    
    Camera cam;
    bool isPressingMove = false;
    bool canShoot = true;
    #endregion

    public static event Action OnHealthChange;
    //public static event Action<DeathType> OnDeath;
    public static event Action<Vector2Int> OnAmmoChange;
    public static event Action<int, PlayerSO> OnGunChange; //The index of the former gun
    public static event Action OnGunPickUp;
    public static event Action OnFire;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        SetStartingWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
        SlideInput();
        ShootInput(); //Check if the player is pressing Fire
        GunChangeInput();
        OrbotersInput();
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, -10);
        transform.up = mousePosition - transform.position;
        if (isPressingMove)
            rb.AddRelativeForce(new Vector2(0, scriptableObject.moveForce));
    }

    #region INPUT METHODS
    void MoveInput()
    {
        isPressingMove = Input.GetKey(KeyCode.Mouse0);
        trailRenderer.emitting = isPressingMove;
        engine.color = (isPressingMove) ? hotEngineColor : coldEngineColor;
    }

    void OrbotersInput()
    {
        if(Input.GetKeyDown(orboterAim))
        {
            scriptableObject.orbiterAim = (OrbiterAim)
                (((int)scriptableObject.orbiterAim + 1) % Enum.GetNames(typeof(OrbiterAim)).Length);
        }
    }
    void SlideInput()
    {
        float d = 1f; //drag
        switch(options.slideInput)
        {
            case SlideInputType.TapToSwap:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    scriptableObject.switchDrag =
                        (scriptableObject.switchDrag == SwitchDrag.Drag) ?
                        scriptableObject.switchDrag = SwitchDrag.Slide :
                        scriptableObject.switchDrag = SwitchDrag.Drag;
                }
                d = (scriptableObject.switchDrag == SwitchDrag.Drag) ? 
                    scriptableObject.Drag.z : scriptableObject.Drag.x;
                break;
            case SlideInputType.HoldToSlide:
                d = (Input.GetKey(KeyCode.Space)) ? scriptableObject.Drag.x : scriptableObject.Drag.z;
                break;
            case SlideInputType.HoldToBrake:
                d = (Input.GetKey(KeyCode.Space)) ? scriptableObject.Drag.z : scriptableObject.Drag.x;
                break;
        }
        rb.drag = (isPressingMove) ? scriptableObject.Drag.y : d;
    }
    void ShootInput()
    {
        switch (scriptableObject.equippedGun.fireType)
        {
            case FireType.Semi:
                if (Input.GetKeyDown(KeyCode.Mouse1)) Fire(); break;
            case FireType.Auto:
                if (Input.GetKey(KeyCode.Mouse1)) Fire(); break;
        }
    }

    void GunChangeInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeGun(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeGun(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeGun(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeGun(3);
    }
    #endregion

    #region SHOOTING METHODS
    void SetStartingWeapon()
    {
        if (scriptableObject.equippedGun)
            return;
        for (int i = 0; i < scriptableObject.Guns.Count; i++)
        {
            if (scriptableObject.Guns[i])
            {
                scriptableObject.equippedGun = scriptableObject.Guns[i];
                return;
            }
        }
        print("Error! No gun found!");
    }

    void ChangeGun(int index)
    {
        if (index >= scriptableObject.Guns.Count)
            return;
        if (!scriptableObject.Guns[index] || scriptableObject.weaponIndex == index)
            return;
        int formerIndex = scriptableObject.weaponIndex;
        scriptableObject.equippedGun = scriptableObject.Guns[index];
        scriptableObject.weaponIndex = index;
        OnGunChange?.Invoke(formerIndex, scriptableObject);
    }

    void Fire()
    {
        GunSO gun = scriptableObject.equippedGun;
        if ((gun.fireType == FireType.Auto && !canShoot) ||
            (!gun.isInfinite && gun.Ammo.x < 1))
            return;
        if (!gun.isInfinite)
        {
            ChangeAmmo(scriptableObject.equippedGun, -1);
        }

        if (gun.fireType == FireType.Auto)
            StartCoroutine(ResetFiringCooldown());
        GenerateBullets(gun);
        OnFire?.Invoke();
    }

    public void ChangeAmmo(GunSO gun, int amount)
    { 
        gun.Ammo.x = Mathf.Clamp(gun.Ammo.x + amount, 0, gun.Ammo.y);
        if(gun == scriptableObject.equippedGun)
            OnAmmoChange?.Invoke(gun.Ammo);
    }

    void GenerateBullets(GunSO gun)
    {
        Bullet bul = null;
        if (gun.spreadType != SpreadType.Double)
        {
            bul = Instantiate(gun.bulletPrefab, mainFirepoint.position, mainFirepoint.rotation)
            .GetComponent<Bullet>();
            bul.AttributeBulletValues(gun);
        }
        if(gun.spreadType == SpreadType.Double || gun.spreadType == SpreadType.Triple)
        {
            for(int i = 0; i < 2; i++)
            {
                bul = Instantiate(gun.bulletPrefab, sideFirePoints[i].position, 
                    sideFirePoints[i].rotation).GetComponent<Bullet>();
                bul.AttributeBulletValues(gun);
            }
        }
        if (gun.spreadType == SpreadType.Diagonal)
        {
            for (int i = 0; i < 2; i++)
            {
                bul = Instantiate(gun.bulletPrefab, diagonalFirePoints[i].position,
                    diagonalFirePoints[i].rotation).GetComponent<Bullet>();
                bul.AttributeBulletValues(gun);
            }
        }
    }

    IEnumerator ResetFiringCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(scriptableObject.equippedGun.fireRate);
        canShoot = true;
    }


    public void FillAmmo()
    {
        foreach (GunSO gun in scriptableObject.Guns)
            if(gun)
                gun.Ammo.x = gun.Ammo.y;
    }

    public void PickGunUp(GunSO gun)
    {
        foreach (GunSO g in scriptableObject.Guns)
        {
            if (g == gun)
            {
                ChangeAmmo(g, g.recoversFromPickUp);
                return;
            }
        }
        for(int i = 0; i < scriptableObject.Guns.Count; i++)
        {
            if(scriptableObject.Guns[i] == null)
            {
                scriptableObject.Guns[i] = gun;
                gun.Ammo.x = gun.Ammo.y;
                break;
            }
        }
        OnGunPickUp?.Invoke();
    }
    #endregion

    #region HEALTH METHODS
    public void ChangeHealth(float value)
    {
        scriptableObject.Health.x = Mathf.Clamp(scriptableObject.Health.x + 
            value, 0, scriptableObject.Health.y);
        OnHealthChange?.Invoke();
        if (scriptableObject.Health.x <= 0)
            Die();
    }

    void Die()
    {
        switch(options.deathType)
        {
            case DeathType.Casual:
                scriptableObject.Health.x = scriptableObject.Health.y;
                FillAmmo();
                return;

            case DeathType.Hardcore:
                gameData.ResetData();
                scriptableObject.ResetData();
                PlayerPrefs.SetInt("Has Save", 0);
                break;
        }
        gameObject.SetActive(false);
        StartCoroutine(WaitToReload());
    }

    IEnumerator WaitToReload()
    {
        yield return new WaitForSeconds(1f);
        TransitionManager.Instance.TransitionToScene(0);
    }
    #endregion
}
