using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PickUpType { Health, Ammo }
public class PickUp : MonoBehaviour
{
    [SerializeField] PickUpType type = PickUpType.Health;
    [SerializeField] float healthValue = 20f;
    [SerializeField] PlayerSO player = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        switch (type)
        {
            case PickUpType.Health:
                if (player.Health.x == player.Health.y)
                    return;
                Player.Instance.ChangeHealth(healthValue);
                break;
            case PickUpType.Ammo:
                if (player.equippedGun.isInfinite || player.equippedGun.Ammo.x == player.equippedGun.Ammo.y)
                    return;
                Player.Instance.ChangeAmmo(player.equippedGun, player.equippedGun.recoversFromPickUp);
                break;
        }

        Destroy(gameObject);
    }
}
