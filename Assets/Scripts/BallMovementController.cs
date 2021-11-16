using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    public float BounceVelocity = 2F;

    private new Rigidbody rigidbody;
    private Vector3 velocity = new Vector3(0F, 0.5F, 0F);

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTER");
        rigidbody.velocity *= -1;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION ENTER");
    }
}
