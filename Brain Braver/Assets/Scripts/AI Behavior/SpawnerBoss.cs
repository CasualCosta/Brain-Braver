using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBoss : MonoBehaviour
{
    [SerializeField] GameObject SpawnerPrefab = null;
    [SerializeField] Transform[] spawnTargets = null;
    [SerializeField] float checkInterval = 2f, spawnDelay = 10f, waitAfterConfirmation = 5f;

    [SerializeField] GameObject[] Spawners;
    // Start is called before the first frame update
    void Start()
    {
        Spawners = new GameObject[spawnTargets.Length];
        StartCoroutine(CheckForSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CheckForSpawn()
    {
        while(true)
        {
            for(int i = 0; i < spawnTargets.Length;i++)
            {
                if (Spawners[i] == null)
                {
                    StartCoroutine(SpawnSpawner(i));
                    yield return new WaitForSeconds(waitAfterConfirmation);
                }
                yield return new WaitForSeconds(checkInterval);
            }
        }
    }

    IEnumerator SpawnSpawner(int i)
    {
        yield return new WaitForSeconds(spawnDelay);
        Spawners[i] = Instantiate(SpawnerPrefab, transform.position, transform.rotation);
        Spawners[i].GetComponent<FollowTarget>().target = spawnTargets[i];
    }

    private void OnDisable()
    {
        for (int i = 0; i < Spawners.Length; i++)
            Destroy(Spawners[i]);
    }
}
