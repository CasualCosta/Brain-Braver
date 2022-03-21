using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    protected virtual void FixedUpdate()
    {
        transform.up = Player.Instance.transform.position - transform.position;
    }
}
