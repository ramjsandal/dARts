using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        if (other != null && other.CompareTag("DartTip"))
        {
            scoreManager.AddPoints(points);
            other.transform.GetComponentInParent<DartBehavior>().FreezeDart();
        }
    }
}
