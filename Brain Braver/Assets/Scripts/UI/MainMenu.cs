using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] OptionsSO m_currentOptions = null;
    [SerializeField] Animator m_anim = null;
    [SerializeField] PlayerSO m_currentPlayer = null;
    [SerializeField] GameDataSO m_gameData = null;
    [SerializeField] Button m_continueButton = null;

    GameData saveData;
    //public static event Action<int> OnStart;


    private void Start()
    {
        saveData = SaveSystem.LoadProgress();
        m_gameData.SetGameData(saveData);
        m_continueButton.interactable = (saveData != null);
        m_currentPlayer.SetPlayerData(saveData);
    }
    public void StartNewGame(int type)
    {
        m_currentOptions.deathType = (DeathType)type;
        m_currentOptions.cameraSize = 1;
        m_currentPlayer.ResetData();
        m_gameData.ResetData();
        //SaveSystem.DeleteProgress();
        TransitionManager.Instance.TransitionToScene(1);
        //OnStart?.Invoke(1);
    }

    public void ContinueGame()
    {
        TransitionManager.Instance.TransitionToScene(m_gameData.saveRoomIndex);
    }
    

    public void OpenNewMenu() => m_anim.SetInteger("menuIndex", 2);
    public void OpenOptions() => m_anim.SetInteger("menuIndex", 1);
    public void OpenMainMenu() => m_anim.SetInteger("menuIndex", 0);

    public void QuitGame()
    {
        Application.Quit();
    }
}
