using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckForEnemies : MonoBehaviour
{
    GameState m_GameState;
    [SerializeField]
    TextMeshProUGUI textHolder;

    void Start()
    {
        m_GameState = FindObjectOfType<GameState>();
    }

    void Update()
    {
        textHolder.text = m_GameState.enemies.Length.ToString();
    }
}
