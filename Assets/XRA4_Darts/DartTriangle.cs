using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTriangle : MonoBehaviour
{
    [SerializeField]
    private int points;

    private ScoreManager scoreManager;
    private void Start()
    {
        scoreManager = ScoreManager.Instance; 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Dart"))
        {
            scoreManager.AddPoints(points); 
        }
    }
}
