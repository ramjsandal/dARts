using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }

    public bool playable;
    public bool started;

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

    public GameObject startUI;
    public GameObject resetUI;
    public GameObject gameDataReset;

    public AudioClip win;
    public AudioClip lose;

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
        playable = true;
        started = false;
        gameDataReset.SetActive(false);

    }

    private void Update()
    {
        if (started && CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
        } else if (started && CurrentTime <= 0)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        started = false;
        if (CurrentScore > highScore.integer)
        {
           AudioSource.PlayClipAtPoint(win, transform.position); 
            highScore.integer = CurrentScore;
            HighScore = highScore;
        } else
        {
            AudioSource.PlayClipAtPoint(lose, transform.position);
        }
    }
    public void AddPoints(int points)
    {
        if (started)
        {
            CurrentScore += points;
        }
    }

    public void StartGame()
    {
        started = true;
        startUI.SetActive(false);
        resetUI.SetActive(true);
    }

    public void ResetGame()
    {
        highScore.integer = 0;
        GameObject.FindObjectOfType<CustomSpatialAnchor>().EraseAnchor();
        playable = false;
        started = false;
        gameDataReset.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
