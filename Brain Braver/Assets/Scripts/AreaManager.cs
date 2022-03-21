using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] Transform spawnPointHolder = null;
    [SerializeField] GameObject playerPrefab = null;
    [SerializeField] GameObject cameraPrefab = null;
    [SerializeField] GameObject transitionCanvas = null;
    [SerializeField] GameObject pauseCanvas = null;
    [SerializeField] GameObject gameUIPrefab = null;
    [SerializeField] GameObject eventSystem = null;
    [SerializeField] CrossSceneSO crossScene = null;

    private void Awake()
    {
        if (crossScene.spawnPointIndex < 0 ||
            crossScene.spawnPointIndex >= spawnPointHolder.childCount)
        {
            crossScene.spawnPointIndex = 0;
            print("Spawn point out of index. Reseting.");
        }
        Transform spawnPoint = spawnPointHolder.GetChild(crossScene.spawnPointIndex);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(cameraPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(transitionCanvas, spawnPoint.position, spawnPoint.rotation);
        Instantiate(pauseCanvas, spawnPoint.position, spawnPoint.rotation);
        Instantiate(gameUIPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(eventSystem, spawnPoint.position, spawnPoint.rotation);
    }
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
