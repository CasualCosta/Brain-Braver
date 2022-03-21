using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedType { FixedSpeed = 0, Rubberband = 1 }
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] SpeedType speedType = SpeedType.FixedSpeed;
    [SerializeField] float fixedSpeed = 50f;
    [SerializeField] float rubberBandRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveSpeed = (speedType == SpeedType.FixedSpeed) ? fixedSpeed : (0.5f + 
            rubberBandRate) * Vector3.Distance(transform.position, Player.Instance.transform.position);
        transform.position = Vector3.MoveTowards
            (transform.position, Player.Instance.transform.position, moveSpeed * Time.fixedDeltaTime);
    }
}
