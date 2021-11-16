using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BallController BallPrefab;

    void OnEnable()
    {
        OnDisable();
        BoundaryDeathController.OnDeath += HandleOnDeath;
        GameController.OnGameInitialize += HandleOnGameInitialize;
    }

    void OnDisable()
    {
        BoundaryDeathController.OnDeath -= HandleOnDeath;
        GameController.OnGameInitialize -= HandleOnGameInitialize;
    }

    private void HandleOnGameInitialize(object sender, GameInitializeEventArgs e)
    {
        InitializeBall();
    }

    private void HandleOnDeath(object sender, BoundaryDeathEventArgs e)
    {
        InitializeBall();
    }

    private void InitializeBall()
    {
        var ballInstance = Instantiate(BallPrefab);
        ballInstance.Player = this;
        ballInstance.transform.position = new Vector3
        {
            x = transform.position.x,
            y = transform.position.y + (ballInstance.Collider.bounds.size.y / 2) + 0.05F,
            z = transform.position.z
        };
        ballInstance.FollowPlayerOffset = ballInstance.transform.position - transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
