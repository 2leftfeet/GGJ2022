using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    
    void Awake()
    {
        current = this;
    }

    public event Action onPlayerTakeDamageEvent;

    public void PlayerTakeDamageEvent()
    {
        onPlayerTakeDamageEvent?.Invoke();
    }

    public event Action onPlayerAddHealthEvent;

    public void PlayerAddHealthEvent()
    {
        onPlayerAddHealthEvent?.Invoke();
    }
    
    public event Action onPlayerDeathEvent; //When Player Dies

    public void PlayerDeathEvent()
    {
        onPlayerDeathEvent?.Invoke();
    }
    
    public event Action onSceneRestartEvent; //When Scene should Restart

    public void SceneRestartEvent()
    {
        onSceneRestartEvent?.Invoke();
    }
    
    public event Action onSceneLoadEvent; //When Scene should load Next scene

    public void SceneLoadEvent()
    {
        onSceneLoadEvent?.Invoke();
    }
    
    public event Action onLevelFinishEvent; //When Player Finished a level

    public void LevelFinishEvent()
    {
        onLevelFinishEvent?.Invoke();
    }
    
    public event Action onPauseMenuEvent; //When Player Finished a level

    public void PauseMenuEvent()
    {
        onPauseMenuEvent?.Invoke();
    }
    
    public event Action onUnpauseMenuEvent; //When Player Finished a level

    public void UnpauseMenuEvent()
    {
        onUnpauseMenuEvent?.Invoke();
    }
}