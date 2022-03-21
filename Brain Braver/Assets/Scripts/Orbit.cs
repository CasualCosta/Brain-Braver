using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public static Orbit Instance { get; private set; }
    [SerializeField] List<Transform> Rigs = new List<Transform>();
    [SerializeField] List<FollowTarget> Orbiters = new List<FollowTarget>();
    [SerializeField] GameObject orbiterPrefab = null;
    [SerializeField] GameObject rigPrefab = null;
    [SerializeField] PlayerSO playerSO = null;
    [SerializeField] float adjustTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SetRigs();
        SpawnOrboters(playerSO.orbiters);
        Instance = this;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            AddOrboter();
    }

    void SetRigs()
    {
        int amount = playerSO.orbiters - Rigs.Count;
        for(int i = 0; i < amount; i++)
        {
            GameObject instance = Instantiate(rigPrefab, transform);
            Rigs.Add(instance.transform);
        }
        for (int i = 0; i < Rigs.Count; i++)
            SetRotation(Rigs[i].transform, 360 / Rigs.Count * i);
    }
    void SpawnOrboters(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            FollowTarget instance = Instantiate(orbiterPrefab.GetComponent<FollowTarget>(), transform);
            instance.transform.position = Player.Instance.transform.position;
            Orbiters.Add(instance);
        }
        for(int i = 0; i < playerSO.orbiters; i++)
            Orbiters[i].target = Rigs[i].GetChild(0);
    }
    void SetRotation(Transform rig, float rotationTarget)
    {
        rig.eulerAngles = new Vector3(0, 0, rotationTarget);
    }
    public void AddOrboter()
    {
        playerSO.orbiters++;
        SetRigs();
        SpawnOrboters(1);
    }
}
