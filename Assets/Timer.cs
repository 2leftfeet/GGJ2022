using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float m_Time = 0;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestTimeText;
    string m_ID;

    bool timerRunning;
    
    void Start()
    {
        timerRunning = true;
        CheckHighScore();
        GameEvents.current.onLevelFinishEvent += StopTimer;
    }

    void CheckHighScore()
    {
        m_ID = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
        if (PlayerPrefs.HasKey(m_ID))
        {
            float bestTime = PlayerPrefs.GetFloat(m_ID);
            bestTimeText.text = string.Format("{0:00}:{1:00}:{2:00}", (int)bestTime/60, (int)bestTime%60, ((int)(bestTime * 100)) % 100);
        }
        else
        {
            bestTimeText.text = "--";
        }
    }

    public void Update()
    {
        if(timerRunning)
        {
            m_Time += Time.deltaTime;
            
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", (int)m_Time/60, (int)m_Time%60, ((int)(m_Time * 100)) % 100);

        }
    }
    public void StopTimer()
    {
        timerRunning = false;
        if (PlayerPrefs.GetFloat(m_ID) > m_Time || !PlayerPrefs.HasKey(m_ID))
        {
            SetHighscore();
        }
    }

    public void SetHighscore()
    {
        PlayerPrefs.SetFloat(m_ID, m_Time);
    }

}
