using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DartSpawner : MonoBehaviour
{
    public GameObject dart;
    public GameObject dartHolder;
    public Transform spawnlocation;
    void Update()
    {
        int numDarts = GameObject.FindGameObjectsWithTag("Dart").Where(a => !a.GetComponent<DartBehavior>().hit).Count();
        if (numDarts < 3)
        {
            SpawnDart();
        }

    }

    void SpawnDart()
    {
        float xOffset = Random.value / 40f;
        float yOffset = Random.value / 40f;

        xOffset = (Random.value > .5f) ? -xOffset : xOffset;
        yOffset = (Random.value > .5f) ? -yOffset : yOffset;

        Vector3 offset = new Vector3(xOffset, 0, yOffset);

        var rot = spawnlocation.rotation.eulerAngles;
        rot.x -= 90;
        
        var ob = Instantiate(dart, spawnlocation.position + offset, Quaternion.Euler(rot.x, rot.y, rot.z));
        ob.transform.parent = dartHolder.transform;
    }
}
