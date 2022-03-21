using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    [SerializeField] GameDataSO m_gameData = null;
    [SerializeField] PlayerSO m_playerData = null;
    [SerializeField] float m_approachSpeed = 2f;
    [SerializeField] float m_transitionDuration = 1f;
    //[SerializeField] int m_spawnPointIndex = 1;

    float m_originalTime;
    bool m_isProcessing = false;
    bool m_justLoaded = true;

    public static event Action OnSaveComplete;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || m_justLoaded)
            return;
        StartCoroutine(DrawPlayer());
    }

    IEnumerator DrawPlayer()
    {
        Player.Instance.enabled = false;
        Transform player = Player.Instance.transform;
        Player.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        while(player.position != transform.position)
        {
            player.position = Vector3.MoveTowards
                (player.position, transform.position, m_approachSpeed * Time.fixedDeltaTime);
            yield return null;
        }
        m_originalTime = Time.timeScale;
        StartCoroutine(AlterTime(true));
        yield return new WaitForSecondsRealtime(m_transitionDuration);
        Refill();
        SaveGame();
        while (m_isProcessing)
            yield return null;
        Player.Instance.enabled = true;
        StartCoroutine(AlterTime(false));
    }

    IEnumerator AlterTime(bool isStopping)
    {
        float target = isStopping ? 0 : m_originalTime;
        while(Time.timeScale != target)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, target, 1 / m_transitionDuration * Time.unscaledDeltaTime);
            yield return null;
        }
    }

    void Refill()
    {
        Player.Instance.ChangeHealth(99999f);
        Player.Instance.FillAmmo();
    }

    void SaveGame()
    {
        int spawnPointIndex = transform.GetSiblingIndex();
        SaveSystem.SaveProgress(m_gameData, m_playerData, spawnPointIndex);
        OnSaveComplete?.Invoke();
    }

    private void Start()
    {
        StartCoroutine(EnableSaving());
    }
    IEnumerator EnableSaving()
    {
        yield return new WaitForSeconds(0.25f);
        m_justLoaded = false;
    }
}
