using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiterPickUp : LastingPickUp
{
    protected override void CauseEffect()
    {
        base.CauseEffect();
        Orbit.Instance.AddOrboter();
    }
}
