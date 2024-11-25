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
            var db = other.transform.GetComponentInParent<DartBehavior>();
            if (!db.hit)
            {
                scoreManager.AddPoints(points);
                other.transform.GetComponentInParent<DartBehavior>().FreezeDart();
            }

        }
    }
}
