using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : LookAtPlayer
{
    [SerializeField] protected Rigidbody2D rb = null;
    [SerializeField] protected float moveForce = 5;
    public float GetMoveForce() => moveForce;
    [HideInInspector] public Vector3 OriginalPosition { get; private set; }
    // Start is called before the first frame update
    void zStart()
    {
        OriginalPosition = transform.position;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        rb.AddRelativeForce(Vector2.up * moveForce);
    }
}
