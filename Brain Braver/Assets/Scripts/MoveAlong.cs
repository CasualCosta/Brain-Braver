using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlong : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Vector3 offset = Vector3.zero;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
