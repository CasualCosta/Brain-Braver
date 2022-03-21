using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : MonoBehaviour
{
    [SerializeField] ChasePlayer chase = null;
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] float minDistance = 12f;

    Vector3 target;
    float moveForce;
    // Start is called before the first frame update
    void Start()
    {
        target = chase.OriginalPosition;
        moveForce = chase.GetMoveForce();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.up = target - transform.position;
        if(Vector2.Distance(transform.position, target) > minDistance)
            rb.AddRelativeForce(Vector2.up * moveForce);
    }
}
