using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GunSO _gunSO = null;
    [SerializeField] Transform[] _firepoints = null;
    [SerializeField] int _flurrySize = 1;
    [SerializeField] float _intraFlurryInterval = 0.2f;
    [SerializeField] float _extraFlurryInterval = 3.5f;
    [SerializeField] float _startingWait = 2f;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Shoot());
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_startingWait);
        while(true)
        {
            for(int i = 0; i < _flurrySize; i++)
            {
                foreach (Transform t in _firepoints)
                {
                    Bullet b = Instantiate(_gunSO.bulletPrefab, t.position, t.rotation).GetComponent<Bullet>();
                    b.AttributeBulletValues(_gunSO);
                }
                yield return new WaitForSeconds(_intraFlurryInterval);
            }
            yield return new WaitForSeconds(_extraFlurryInterval);
        }
    }
}
