using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TriggerType { OnSceneLoad, OnTriggerEnter, OnTriggerExit, OnDisable }
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] DialogueSO dialogue = null;
    [SerializeField] RoomSO room = null;
    [SerializeField] TriggerType type = TriggerType.OnSceneLoad;
    [SerializeField] float triggerDelay = 0f;

    [Tooltip("Use a negative number to mean it is repeatable")]
    [SerializeField] int eventIndex = 0;

    //The dialogue has to be passed. The room can be null, the float is the
    //wait time and the int is the eventIndex.
    public static event Action <DialogueSO, RoomSO, float, int> OnDialogueTrigger;
    public void TriggerDialogue()
    {
        if (eventIndex >= 0 && eventIndex < room.storyEvents.Length)
        {
            if (room.storyEvents[eventIndex].index > 0)
                return;
        }
        OnDialogueTrigger?.Invoke(dialogue, room, triggerDelay, eventIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        if (type == TriggerType.OnTriggerEnter)
            TriggerDialogue();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        if (type == TriggerType.OnTriggerExit)
            TriggerDialogue();
    }

    private void Start()
    {
        if (type == TriggerType.OnSceneLoad)
            TriggerDialogue();
    }

    private void OnDisable()
    {
        if (type == TriggerType.OnDisable)
            TriggerDialogue();
    }
}
