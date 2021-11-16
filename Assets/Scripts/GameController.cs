using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartEventArgs : EventArgs { }

public class GameInitializeEventArgs : EventArgs { }

public class GameController : MonoBehaviour
{
    public static event EventHandler<GameInitializeEventArgs> OnGameInitialize;
    public static event EventHandler<GameStartEventArgs> OnGameStart;

    public BoundaryController[] BoundariesPrefab;
    public BallController BallPrefab;
    public PlayerController PlayerPrefab;

    public Vector2 PlayerSpawnPosition;

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
        InitializeGame();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeGame()
    {
        foreach (var boundary in BoundariesPrefab)
        {
            var boundaryInstance = Instantiate(boundary);
            boundaryInstance.ZPosition = ZPosition;
        }
        var playerInstance = Instantiate(PlayerPrefab, new Vector3(PlayerSpawnPosition.x, PlayerSpawnPosition.y, ZPosition), Quaternion.identity);
        OnGameInitialize?.Invoke(this, new GameInitializeEventArgs());
    }

    private void StartGame()
    {
        OnGameStart?.Invoke(this, new GameStartEventArgs());
    }
}
