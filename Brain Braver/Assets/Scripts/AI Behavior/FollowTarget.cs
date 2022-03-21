using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    [SerializeField] SpeedType speedType = SpeedType.FixedSpeed;
    [SerializeField] float fixedSpeed = 1f;
    [SerializeField] float rubberBandRate = 0.1f;
    [SerializeField] float rubberBandMultiplier = 0.1f;
    [SerializeField] float minimumDistance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = (speedType == SpeedType.FixedSpeed) ? fixedSpeed : (0.5f +
            rubberBandRate) * Vector2.Distance(transform.position, Player.Instance.transform.position)
            * rubberBandMultiplier;
        if (Vector2.Distance(transform.position, target.position) > minimumDistance)
            transform.position = Vector3.MoveTowards
                (transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
