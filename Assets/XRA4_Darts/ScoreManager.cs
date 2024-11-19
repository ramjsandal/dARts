using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }

    [SerializeField]
    private float timeLimit;

    [SerializeField]
    private TMP_Text highScoreUI;
    [SerializeField]
    private TMP_Text currentScoreUI;
    [SerializeField]
    private TMP_Text timerUI;

    [SerializeField]
    private IntSO highScore;
    public IntSO HighScore
    {
        get { return highScore; }
        set
        {
            highScore = value;
            highScoreUI.text = highScore.integer.ToString();
        }
    }

    private int currentScore;
    public int CurrentScore
    {
        get { return currentScore; }
        set
        {
            currentScore = value;
            currentScoreUI.text = currentScore.ToString();
        }
    }

    private float currentTime;
    public float CurrentTime
    {
        get { return currentTime; }
        set
        {
            currentTime = value;
            int minutes = (int) currentTime / 60;
            int seconds = (int) currentTime % 60;
            timerUI.text = minutes.ToString("0") + ":" + seconds.ToString("00");
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }

        HighScore = highScore;
        CurrentScore = 0;
        CurrentTime = timeLimit;

    }

    public void AddPoints(int points)
    {
        currentScore += points;
    }

}
