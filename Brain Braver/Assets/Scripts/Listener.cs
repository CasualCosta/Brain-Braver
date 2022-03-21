using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Trigger { OnTriggerEnter, OnTriggerExit, OnEnemyDeath }
//Abstract class to listen to the Announcer
public abstract class Listener : MonoBehaviour
{
    [SerializeField] protected MonoBehaviour[] _scripts = null;
    [SerializeField] protected Trigger trigger = Trigger.OnTriggerEnter;
    [SerializeField] protected List<EnemyHealth> enemies = new List<EnemyHealth>();
    [SerializeField] protected int enterIndex = -1;

    protected abstract void ListenToEvent(int i);
    protected abstract void ListenToEvent();

    private void OnEnable()
    {
        switch (trigger)
        {
            case (Trigger.OnTriggerEnter): Announcer.EnterEvent += ListenToEvent; break;
            case (Trigger.OnEnemyDeath):
                foreach(EnemyHealth health in enemies)
                {
                    health.OnBossDeath += ListenToEvent;
                }
                break;
        }
    }

    private void OnDisable()
    {
        switch(trigger)
        {
            case (Trigger.OnTriggerEnter): Announcer.EnterEvent -= ListenToEvent; break;
        }
        
    }
}
