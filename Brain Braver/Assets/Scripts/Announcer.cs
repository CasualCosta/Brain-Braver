using System;
using System.Collections.Generic;
using UnityEngine;

public class Announcer : MonoBehaviour
{
    [SerializeField] Trigger trigger = Trigger.OnTriggerEnter;
    [SerializeField] int enterIndex = 0;

    public static event Action<int> EnterEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (trigger != Trigger.OnTriggerEnter)
            return;
        if(collision.CompareTag("Player"))
            EnterEvent?.Invoke(enterIndex);
    }
}
