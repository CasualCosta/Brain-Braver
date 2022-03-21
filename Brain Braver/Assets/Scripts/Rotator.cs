using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 1f;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.fixedDeltaTime);
    }
}
