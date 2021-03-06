using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivity : MonoBehaviour
{
    PlayerMovement m_Player;
    Slider slider;

    void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("MouseSensitivity"));
        slider = GetComponent<Slider>();
        m_Player = FindObjectOfType<PlayerMovement>();
        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            m_Player.sensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
            if (slider != null) slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        }
    }

    public void UpdateSensitivity(Slider slider)
    {
        m_Player.sensitivity = slider.value;
        PlayerPrefs.SetFloat("MouseSensitivity",m_Player.sensitivity);
    }
}
