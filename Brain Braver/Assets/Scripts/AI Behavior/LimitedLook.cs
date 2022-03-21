using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLook : LookAtPlayer
{
    [SerializeField] Vector2 rotationLimits = Vector2.zero;
    [Tooltip("Behaviors that are enabled or disabled when you the rotation is on the edge.")]
    [SerializeField] List<MonoBehaviour> toggableBehaviours = new List<MonoBehaviour>();

    bool onTheEdge = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        float zed = Mathf.Clamp(transform.localEulerAngles.z, rotationLimits.x, rotationLimits.y);
        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, zed);
        if((zed == rotationLimits.x || zed == rotationLimits.y) && !onTheEdge)
        {
            onTheEdge = true;
            foreach (MonoBehaviour mono in toggableBehaviours)
                mono.enabled = false;
        }
        else if((zed > rotationLimits.x && zed < rotationLimits.y) && onTheEdge)
        {
            onTheEdge = false;
            foreach (MonoBehaviour mono in toggableBehaviours)
                mono.enabled = true;
        }

    }
}
