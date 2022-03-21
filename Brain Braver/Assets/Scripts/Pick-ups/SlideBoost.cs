using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBoost : LastingPickUp
{
    [SerializeField] float value = 0.025f;
    protected override void CauseEffect()
    {
        base.CauseEffect();
        player.Drag.x -= value;
    }
}
