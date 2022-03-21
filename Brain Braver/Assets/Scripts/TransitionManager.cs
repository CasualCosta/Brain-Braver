using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance { get; private set; }
    [SerializeField] Canvas m_canvas = null;
    [SerializeField] Image m_panel = null;
    [SerializeField] CrossSceneSO m_cross = null;
    [SerializeField] GameDataSO m_gameData = null;
    [SerializeField] TransitionTextSO m_transitionText = null;
    [SerializeField] Text m_loadText = null;
    [SerializeField] Text m_startText = null;
    [SerializeField] float m_transitionTime = 1f;
    [SerializeField] float m_waitTime = 2f;

    public static bool m_isTransitiong = false;

    private void Awake()
    {
        Instance = this;
    }

    /*
    private void OnEnable()
    {
        AreaTransition.OnTransitionEnter += TransitionToScene;
        MainMenu.OnStart += TransitionToScene;
        Player.OnDeath += TransitionToScene;
    }
    private void OnDisable()
    {
        AreaTransition.OnTransitionEnter -= TransitionToScene;
        MainMenu.OnStart -= TransitionToScene;
        Player.OnDeath += TransitionToScene;
    }
    */

    public void TransitionToScene(int sceneIndex)
    {
        StartCoroutine(TransitionCoroutine(sceneIndex, true));
    }
    void TransitionToScene(DeathType deathType)
    {
        if(deathType != DeathType.Casual)
            StartCoroutine(TransitionCoroutine(0, true));
    }

    IEnumerator TransitionCoroutine(int sceneIndex, bool leavingScene)
    {
        m_isTransitiong = true;
        float target = (leavingScene) ? 1 : 0;
        
        m_canvas.enabled = true;

        if (leavingScene)
            SetInfoText(sceneIndex);
        
        m_loadText.enabled = true;
        m_startText.enabled = false;
        
        while(m_panel.color.a != target)
        {
            float current = Mathf.MoveTowards(m_panel.color.a, target, 1 / 
                m_transitionTime * Time.unscaledDeltaTime);
            Color cp = m_panel.color;
            Color ct = m_loadText.color;
            ct.a = cp.a = current;
            m_panel.color = cp;
            m_loadText.color = ct;
            Time.timeScale = 1 - current;
            //yield return new WaitForSecondsRealtime(0.02f);
            yield return null;
        }
        if (leavingScene)
        {
            yield return new WaitForSecondsRealtime(m_waitTime);
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            m_canvas.enabled = false;
            m_isTransitiong = false;    
        }
    }

    IEnumerator WaitForInput()
    {
        while(!Input.anyKeyDown)
            yield return null;
        StartCoroutine(TransitionCoroutine(m_cross.spawnPointIndex, false));
    }
    void SetInfoText(int sceneIndex)
    {
        m_transitionText.currentMessage = 
        m_loadText.text = m_gameData.rooms[sceneIndex].loadMessage == "" ?
            m_transitionText.messages[Random.Range(0, m_transitionText.messages.Length)] :
            m_gameData.rooms[sceneIndex].loadMessage;
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            SetInfoText(0);
        else
            m_loadText.text = m_transitionText.currentMessage;
        Time.timeScale = 0f;
        m_canvas.enabled = true;
        StartCoroutine(WaitForInput());
    }
}
