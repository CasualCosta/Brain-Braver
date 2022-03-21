using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Canvas m_pauseCanvas = null;
    [SerializeField] GameObject m_confirmPanel = null;
    [SerializeField] Text m_pausedText = null;
    [SerializeField] Image m_shade = null;
    [SerializeField] Animator m_animator = null;
    [SerializeField] float m_animationTime = 1f;
    [SerializeField] float m_transitionTime = 1f;

    public static bool m_isPaused = false;
    bool m_isTransitioning = false;
    float m_transparencyPercentage;
    float m_prePauseTimeScale;
    public void ReturnToGame()
    {
        ManagePause();
    }

    public void OpenOptions()
    {
        m_confirmPanel.SetActive(false);
        m_animator.SetBool("openOptions", true);
    }

    public void QuitGame()
    {
        m_confirmPanel.SetActive(true);
    }
    
    public void ConfirmQuit()
    {
        TransitionManager.Instance.TransitionToScene(0);
    }

    public void ReturnToMenu()
    {
        print("Back to menu."); 
        m_confirmPanel.SetActive(false);
        m_animator.SetBool("openOptions", false);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_transparencyPercentage = m_shade.color.a / 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (TransitionManager.m_isTransitiong)
            return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!m_isTransitioning)
                ManagePause();
        }
    }

    void ManagePause()
    {
        //print("Managing pause.");
        if (m_isTransitioning)
            return;
        m_isPaused = !m_isPaused;
        m_isTransitioning = true;
        StartCoroutine(PauseTransition());
    }

    IEnumerator PauseTransition()
    {
        if (m_isPaused)
            m_prePauseTimeScale = Time.timeScale;
        float targetSpeed = (m_isPaused) ? 0 : m_prePauseTimeScale;
        m_pauseCanvas.enabled = true;
        m_animator.enabled = true;
        if(!m_isPaused)
        {
            m_animator.SetBool("isOpen", false);
            yield return new WaitForSecondsRealtime(m_animationTime);
        }
        while(Time.timeScale != targetSpeed)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, targetSpeed, 1 / m_transitionTime * Time.unscaledDeltaTime);
            Color cp = m_shade.color;
            Color ct = m_pausedText.color;
            cp.a = m_transparencyPercentage - Time.timeScale * m_transparencyPercentage;
            ct.a = 1 - Time.timeScale;
            m_shade.color = cp;
            m_pausedText.color = ct;
            yield return null;
        }
        if(m_isPaused)
        {
            m_animator.SetBool("isOpen", true);
            yield return new WaitForSecondsRealtime(m_animationTime);
        }
        else
        {
            m_pauseCanvas.enabled = false;
            m_animator.enabled = false;
        }
        m_isTransitioning = false;
    }
}
