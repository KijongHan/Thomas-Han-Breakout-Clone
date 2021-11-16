using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BoundaryController[] BoundariesPrefab;
    public BallController BallPrefab;
    public PlayerController PlayerPrefab;

    // Z Position should be universally same across all game objects participating in game loop
    public float ZPosition = 1F;

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
