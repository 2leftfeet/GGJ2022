using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    int m_Time = 0;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestTimeText;
    string m_ID;
    
    void Start()
    {
        CheckHighScore();
        StartTimer();
        GameEvents.current.onLevelFinishEvent += StopTimer;
    }

    void CheckHighScore()
    {
        m_ID = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();
        Debug.Log(m_Time);
        if (PlayerPrefs.HasKey(m_ID))
        {
            bestTimeText.text = PlayerPrefs.GetInt(m_ID).ToString();
        }
        else
        {
            bestTimeText.text = "0";
        }
    }

    public void StartTimer()
    {
        m_Time = 0;
        InvokeRepeating("IcrementTime",1,1);
    }

    public void StopTimer()
    {
        CancelInvoke();
        if (PlayerPrefs.GetInt(m_ID) > m_Time || PlayerPrefs.GetInt(m_ID) == 0)
        {
            SetHighscore();
        }
    }

    public void SetHighscore()
    {
        PlayerPrefs.SetInt(m_ID, m_Time);
    }

    void IcrementTime()
    {
        m_Time += 1;
        timerText.text = m_Time.ToString();
    }
}
