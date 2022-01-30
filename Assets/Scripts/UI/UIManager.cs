using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject levelCompleteUI;
    
    [SerializeField]
    GameObject restartUI;
    
    [SerializeField]
    GameObject pauseUI;

    [SerializeField]
    GameObject consumeUI;

    void Start()
    {
        GameEvents.current.onPlayerDeathEvent += OpenRestartLevelUI;
        GameEvents.current.onLevelFinishEvent += OpenNextLevelUI;
        GameEvents.current.onPauseMenuEvent += OpenPauseMenu;
        GameEvents.current.onUnpauseMenuEvent += ClosePauseMenu;
        GameEvents.current.onSceneRestartEvent += ResetTimeScale;
        GameEvents.current.onConsumeUIEnterEvent += OpenConsumeUI;
        GameEvents.current.onConsumeUIExitEvent += CloseConsumeUI;
    }

    void OpenRestartLevelUI()
    {
        restartUI.gameObject.SetActive(true);
    }

    void OpenNextLevelUI()
    {
        levelCompleteUI.gameObject.SetActive(true);
    }

    void CheckForNextLevel()
    {
        if (levelCompleteUI.gameObject)
        {
            
        }
    }

    void OpenPauseMenu()
    {
        if (!levelCompleteUI.gameObject.activeInHierarchy)
        {
            pauseUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void ClosePauseMenu()
    {
        pauseUI.gameObject.SetActive(false);
        ResetTimeScale();
    }

    void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        ResetTimeScale();
    }

    void OpenConsumeUI()
    {
        consumeUI.gameObject.SetActive(true);
    }

    void CloseConsumeUI()
    {
        consumeUI.gameObject.SetActive(false);
    }
}
