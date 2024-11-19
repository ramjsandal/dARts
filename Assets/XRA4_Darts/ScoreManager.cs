using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }

    [SerializeField]
    private IntSO highScore;

    private int currentScore;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        } else
        {
            instance = this;
        }

        currentScore = 0;
    }

    public void AddPoints(int points)
    {
        currentScore += points;
    }

}
