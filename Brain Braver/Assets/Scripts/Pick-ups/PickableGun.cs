using System;
using System.Collections.Generic;
using UnityEngine;

public class PickableGun : LastingPickUp
{
    [SerializeField] GunSO sO = null;

    public static event Action OnPickUp;
    protected override void CauseEffect()
    {
        base.CauseEffect();
        Player.Instance.PickGunUp(sO);
        OnPickUp.Invoke();
    }
}
