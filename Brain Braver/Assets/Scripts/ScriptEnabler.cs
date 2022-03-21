using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnabler : Listener
{
    protected override void ListenToEvent(int i)
    {
        foreach(MonoBehaviour mono in _scripts)
            mono.enabled = true;
    }
    protected override void ListenToEvent()
    {
        throw new System.NotImplementedException();
    }
}
