using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LastingPickUp : MonoBehaviour
{
    //[SerializeField] GunSO sO = null;
    [SerializeField] protected PlayerSO player = null;
    [SerializeField] RoomSO roomSO = null;
    [Tooltip("The index of the picked-up variable in the roomSO")]
    [SerializeField] int pickUpIndex = 0;

    // Start is called before the first frame update
    private void Start()
    {
        if (roomSO)
            if (roomSO.pickedPowerUps[pickUpIndex].boolean)
                Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        CauseEffect();
        Destroy(gameObject);
    }

    protected virtual void CauseEffect()
    {
        if (roomSO)
            roomSO.pickedPowerUps[pickUpIndex].boolean = true;
        else
            print("No room detected.");
    }
    public void SetRoomData(RoomSO room, int index)
    {
        roomSO = room;
        pickUpIndex = index;
    }
}
