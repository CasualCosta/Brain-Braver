using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Single spawn means they only spawn once.
enum SpawnType { SingleSpawn, TimeBased }
public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _spawnee = null;
    [SerializeField] SpawnType _spawnType = SpawnType.TimeBased;
    [SerializeField] float _intraWaveInterval = 1.5f;
    [SerializeField] float _intervalBtwnWaves = 15f;
    [SerializeField] int _waveSize = 3;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(SpawnWave());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWave()
    {
       while(true)
        {
            for(int i = 0; i < _waveSize; i++)
            {
                GameObject instance = Instantiate(_spawnee, transform.position, transform.rotation);
                if(i < _waveSize - 1)
                    yield return new WaitForSeconds(_intraWaveInterval);
                DistanceToggler toggler = instance.GetComponent<DistanceToggler>();
                if (toggler != null) //try to activate the enemy if it's fitting.
                    toggler.ToggleActivation(true);
            }
            if (_spawnType == SpawnType.SingleSpawn)
                yield break;
            yield return new WaitForSeconds(_intervalBtwnWaves);
        }
    }
}
