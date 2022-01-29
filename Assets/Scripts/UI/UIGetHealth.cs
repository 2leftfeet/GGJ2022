using System;
using TMPro;
using UnityEngine;

public class UIGetHealth : MonoBehaviour
{
    PlayerHealth m_PlayerHealth;

    TextMeshProUGUI m_PlayerHpTextBox;

    void Start()
    {
        m_PlayerHpTextBox = GetComponent<TextMeshProUGUI>();
        m_PlayerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        GameEvents.current.onPlayerTakeDamageEvent += UpdateUI;
        GameEvents.current.onPlayerAddHealthEvent += UpdateUI;
    }

    void UpdateUI()
    {
        m_PlayerHpTextBox.text = m_PlayerHealth.healthAmount.ToString();
    }
}
