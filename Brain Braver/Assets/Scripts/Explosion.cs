using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public float growthTime = 1f;
    [HideInInspector] public float maxSize = 1f;
    [SerializeField] AudioSource sFX = null;
    // Start is called before the first frame update
    void Start()
    {
        if (sFX.clip)
            sFX.Play();
        transform.localScale = new Vector3(0, 0, 1);
        Destroy(gameObject, growthTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(1, 1, 0) * maxSize / growthTime * Time.deltaTime;
    }
}
