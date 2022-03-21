using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] List<GameObject> objects = null;
    [SerializeField] RoomSO roomSO = null;
    [SerializeField] int eventIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (roomSO.storyEvents[eventIndex].index != 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject gameObject in objects)
            gameObject.SetActive(true);
    }
}
