using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    [SerializeField]
    GameObject levelSelectorUI;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenLevelSelect()
    {
        levelSelectorUI.gameObject.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        levelSelectorUI.gameObject.SetActive(false);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
