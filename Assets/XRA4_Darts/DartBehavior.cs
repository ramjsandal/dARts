using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DartBehavior : MonoBehaviour
{
    public float forceMultiplier = 20f;
    AudioSource source;
    Rigidbody rb;
    OVRInput.Controller rightController = OVRInput.Controller.RTouch;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThrowDart()
    {

        var vel = OVRInput.GetLocalControllerVelocity(rightController).magnitude;
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(transform.forward * vel * forceMultiplier);
        this.transform.SetParent(null);
        source.Play();
        Invoke("DestroyDart", 5f);

    }

    public void PickupDart()
    {
        rb.isKinematic = false;
    }

    void DestroyDart()
    {
        if (!hit)
        {
            Destroy(this.gameObject);
        }
    }
    public bool hit = false;
    public void FreezeDart()
    {
        hit = true;
        var con = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        rb.constraints = con;
    }
}
