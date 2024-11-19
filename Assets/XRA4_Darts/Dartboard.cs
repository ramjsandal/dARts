using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dartboard : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == null && collision.gameObject.CompareTag("Dart"))
        {
            audioSource.Play();
        }
    }

}
