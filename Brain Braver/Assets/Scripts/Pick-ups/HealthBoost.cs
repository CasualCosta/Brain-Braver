using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : LastingPickUp
{
    [Tooltip("The amount of max health increased.")]
    [SerializeField] int boostAmount = 5;

    public static event Action OnPickUp;
    protected override void CauseEffect()
    {
        base.CauseEffect();
        player.Health.y += boostAmount;
        Player.Instance.ChangeHealth(player.Health.y);
        OnPickUp?.Invoke();
    }
}
