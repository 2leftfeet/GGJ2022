using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject levelCompleteUI;
    
    [SerializeField]
    GameObject restartUI;

    void Start()
    {
        GameEvents.current.onPlayerDeathEvent += OpenRestartLevelUI;
        GameEvents.current.onLevelFinishEvent += OpenNextLevelUI;
    }

    void OpenRestartLevelUI()
    {
        restartUI.gameObject.SetActive(true);
    }

    void OpenNextLevelUI()
    {
        levelCompleteUI.gameObject.SetActive(true);
    }

    void PauseGame()
    {
        
    }
}
