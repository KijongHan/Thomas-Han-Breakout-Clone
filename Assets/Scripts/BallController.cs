using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float BounceVelocity = 2F;

    private new Rigidbody rigidbody;
    private Vector3 velocity = new Vector3(0F, 0.5F, 0F);

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        OnDisable();
        BoundaryDeathController.OnDeath += HandleOnDeath;
    }

    void OnDisable()
    {
        BoundaryDeathController.OnDeath -= HandleOnDeath;
    }

    private void HandleOnDeath(object sender, BoundaryDeathEventArgs e)
    {
        throw new NotImplementedException();
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

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION ENTER");
    }
}
