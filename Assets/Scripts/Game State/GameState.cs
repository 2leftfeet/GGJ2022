using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    void Start()
    {
        GameEvents.current.onSceneRestartEvent += ReloadLevel;
        GameEvents.current.onSceneLoadEvent += GoToNextLevel;
    }

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemiesLeft = enemies.Length;
        if(enemiesLeft == 0)
            GameEvents.current.LevelFinishEvent();
    }

    static void ReloadLevel()
    {
        Debug.Log("Restarting Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    static void GoToNextLevel()
    {
        Debug.Log("Going to the next Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
