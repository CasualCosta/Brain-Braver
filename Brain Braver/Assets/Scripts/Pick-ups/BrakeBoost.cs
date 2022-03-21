using System;
using System.Collections.Generic;
using UnityEngine;

public class BrakeBoost : LastingPickUp
{
    [SerializeField] float value = 0.2f;

    public static event Action OnPickUp;
    protected override void CauseEffect()
    {
        base.CauseEffect();
        player.Drag.z += value;
        OnPickUp?.Invoke();
    }
}
