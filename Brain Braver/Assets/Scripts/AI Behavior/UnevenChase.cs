using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnevenChase : ChasePlayer
{
    [SerializeField] protected Vector2 activationInterval = Vector2.one;
    [SerializeField] protected Vector2 durationRange = Vector2.zero;
    [SerializeField] protected float dragMultiplier = 1f, forceMultiplier;

    float duration;
    // Start is called before the first frame update
    void Start()
    {
        duration = Random.Range(activationInterval.x, activationInterval.y);
    }

    protected override void FixedUpdate()
    {
        if(duration < 0)
        {
            duration = Random.Range(activationInterval.x, activationInterval.y);
            StartCoroutine(ChangeDrag());
        }
        duration -= Time.fixedDeltaTime;
        base.FixedUpdate();
    }

    IEnumerator ChangeDrag()
    {
        float originalDrag = rb.drag;
        rb.drag *= dragMultiplier;
        yield return new WaitForSeconds(Random.Range(durationRange.x, durationRange.y));
        rb.drag = originalDrag;
        StartCoroutine(Boost());
    }
    IEnumerator Boost()
    {
        float originalForce = moveForce;
        moveForce *= forceMultiplier;
        yield return new WaitForSeconds(Random.Range(durationRange.x, durationRange.y));
        moveForce = originalForce;
    }
}
