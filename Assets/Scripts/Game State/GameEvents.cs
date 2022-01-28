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
}