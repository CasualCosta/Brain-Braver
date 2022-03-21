using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbotAim : MonoBehaviour
{
    [SerializeField] PlayerSO playerSO = null;
    [SerializeField] Transform firepoint = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(playerSO.orbiterAim)
        {
            case OrbiterAim.Player: transform.rotation = Player.Instance.transform.rotation; break;
            case OrbiterAim.Cursor: transform.up = Camera.main.ScreenToWorldPoint(Input.mousePosition) 
                    - new Vector3(0, 0, -10) - transform.position; break;
        }
    }

    void Fire()
    {
        GunSO gun = playerSO.equippedGun;
        Bullet b = Instantiate(gun.bulletPrefab, firepoint.position, firepoint.rotation).
            GetComponent<Bullet>();
        b.AttributeBulletValues(gun, true);
        b.transform.localScale = transform.localScale;
    }
    private void OnEnable()
    {
        Player.OnFire += Fire;
    }
    private void OnDisable()
    {
        Player.OnFire -= Fire;
    }
}
