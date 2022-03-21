using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleExplosions : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab = null;
    //[Tooltip("Use this to signalized that there is a room event and the explosion is not to be triggered")]
    //[SerializeField] RoomSO room = null;
    //[SerializeField] int enemyIndex = 0;
    [SerializeField] int amount = 10;
    [Tooltip("How off center the explosion can be.")]
    [SerializeField] float spawnOffset = 3f;
    [SerializeField] Vector2 intervalBtwnExplosions = Vector2.one;

    [SerializeField] Vector2 explosionTime = Vector2.one;
    [SerializeField] Vector2 explosionSize = Vector2.one;
    // Start is called before the first frame update
    void Start()
    {
        /*
        if (room.deletedEnemies[enemyIndex].boolean)
            Destroy(gameObject);
        */
        StartCoroutine(ExplodeRepeatedly());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ExplodeRepeatedly()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPoint = Generic.SetRandomCircularPosition(transform.position, spawnOffset);
            Explosion exp = Instantiate(explosionPrefab, spawnPoint, Quaternion.identity).GetComponent<Explosion>();
            exp.maxSize = Random.Range(explosionSize.x, explosionSize.y);
            exp.growthTime = Random.Range(explosionTime.x, explosionTime.y);
            yield return new WaitForSeconds(Random.Range(intervalBtwnExplosions.x, intervalBtwnExplosions.y));
        }
        Destroy(gameObject);
    }
}
