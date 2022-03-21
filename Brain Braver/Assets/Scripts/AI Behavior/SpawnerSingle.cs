using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSingle : MonoBehaviour
{
    [SerializeField] GameObject prefab = null;
    [SerializeField] float checkWait = 1f, spawnDelay = 5f;

    GameObject instance;
    // Start is called before the first frame update
    void OnEnable() => StartCoroutine(Spawn());
    private void OnDisable() => StopAllCoroutines();

    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(checkWait);
            if(!instance)
            {
                yield return new WaitForSeconds(spawnDelay);
                instance = Instantiate(prefab, transform.position, transform.rotation);
            }
        }
    }
    public void OnDestroy()
    {
        Destroy(instance);
    }

}
