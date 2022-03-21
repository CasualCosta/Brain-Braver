using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTransition : MonoBehaviour
{
    //[SerializeField] object myScene;
    [SerializeField] CrossSceneSO cross = null;
    [SerializeField] int sceneIndex = 0;
    [SerializeField] int spawnPointIndex = 0;

    //public static event Action<int> OnTransitionEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        cross.spawnPointIndex = spawnPointIndex;
        TransitionManager.Instance.TransitionToScene(sceneIndex);
        //OnTransitionEnter?.Invoke(sceneIndex);
    }
}
