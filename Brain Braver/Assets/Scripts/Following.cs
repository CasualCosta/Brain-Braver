using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    public static Following Instance { get; private set; }
    [SerializeField] List<FollowTarget> Followers = null;
    [SerializeField] GameObject followerPrefab = null;
    [SerializeField] PlayerSO playerSO = null;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFollowers(playerSO.followers);
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnFollowers(int amount)
    {
        print(amount);
        for(int i = 0; i < amount; i++)
        {
            print("Instantiating.");
            FollowTarget instance = Instantiate(followerPrefab.GetComponent<FollowTarget>(), transform);
            instance.transform.position = Player.Instance.transform.position;
            Followers.Add(instance);
        }
        if (Followers[0] != null)
            Followers[0].target = Player.Instance.transform;
        for (int i = 1; i < Followers.Count; i++)
            Followers[i].target = Followers[i - 1].transform;
    }

    public void AddFollower()
    {
        playerSO.followers++;
        SpawnFollowers(1);
    }
}
