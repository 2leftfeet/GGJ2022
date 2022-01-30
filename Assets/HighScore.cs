using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    string m_ID;
    public int levelId;
    [SerializeField] TextMeshProUGUI textHolder;

    void Start()
    {
        //textHolder = GetComponentInChildren<TextMeshProUGUI>();
        CheckHighScore();
    }

    void CheckHighScore()
    {
        m_ID = "Level " + levelId.ToString();
        if (PlayerPrefs.HasKey(m_ID))
        {
            textHolder.text = PlayerPrefs.GetFloat(m_ID).ToString();
        }
    }
}
