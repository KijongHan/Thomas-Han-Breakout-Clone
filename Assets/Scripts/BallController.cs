using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : NetworkBehaviour
{
    public float VelocityMagnitude = 3F;
    public bool FollowPlayer = true;

    public NetworkIdentity PlayerId { get; set; }
    public Vector3 FollowPlayerOffset { get; set; }
    public Transform FollowTransform { get; set; }
    public Collider Collider { get; private set; }

    private new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }

    void OnEnable()
    {
        OnDisable();
        BoundaryDeathController.OnDeath += HandleOnDeath;
        PlayerFireController.OnFire += HandleOnFire;
    }

    void OnDisable()
    {
        BoundaryDeathController.OnDeath -= HandleOnDeath;
        PlayerFireController.OnFire -= HandleOnFire;
    }

    private void HandleOnFire(object sender, FireEventArgs e)
    {
        if (!isServer) return;
        if (e.PlayerId.netId == PlayerId.netId && FollowPlayer)
        {
            // Try to keep initial and ball fall velocities constant
            var randomAngleRelativeToRightAngle = UnityEngine.Random.Range(-e.InitialFireAngle, e.InitialFireAngle);
            var velocityX = Math.Sin(Mathf.Deg2Rad * Math.Abs(randomAngleRelativeToRightAngle)) * VelocityMagnitude;
            var velocityY = Math.Cos(Mathf.Deg2Rad * Math.Abs(randomAngleRelativeToRightAngle)) * VelocityMagnitude;
            if (randomAngleRelativeToRightAngle < 0) velocityX *= -1;
            rigidbody.velocity = new Vector3((float)velocityX, (float)velocityY, 0F);
            FollowPlayer = false;
        }
    }

    private void HandleOnDeath(object sender, BoundaryDeathEventArgs e)
    {
         if (e.DeadBall == this) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (FollowPlayer && isServer) transform.position = FollowTransform.transform.position + FollowPlayerOffset;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION ENTER");
    }
}
