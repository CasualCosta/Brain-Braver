using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : LastingPickUp
{
    [SerializeField] float value = 1f;

    public static event Action OnPickUp;

    protected override void CauseEffect()
    {
        base.CauseEffect();
        player.moveForce += value;
        OnPickUp?.Invoke();
    }
}
