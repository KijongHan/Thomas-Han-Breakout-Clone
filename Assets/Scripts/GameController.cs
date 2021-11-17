using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartEventArgs : EventArgs { }

public class GameInitializeEventArgs : EventArgs { }

public class GameController : NetworkBehaviour
{
    public static event EventHandler<GameInitializeEventArgs> OnGameInitialize;
    public static event EventHandler<GameStartEventArgs> OnGameStart;

    public BoundaryController[] BoundariesPrefab;
    public BallController BallPrefab;
    public PlayerCharacterController PlayerPrefab;
    public BrickSpawnManagerScriptableObject BrickSpawner;

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
        if (!isServer) return;
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
            NetworkServer.Spawn(boundaryInstance.gameObject);
            boundaryInstance.ZPosition = ZPosition;
        }
        
        foreach (var brickSpawn in BrickSpawner.BrickSpawns)
        {
            var brickInstance = Instantiate(brickSpawn.BrickPrefab);
            NetworkServer.Spawn(brickInstance.gameObject);
            var spawnOffset = new Vector3
            {
                x = BrickSpawner.SpawnReferencePosition.x + (brickSpawn.RowOffset * (brickInstance.BrickCollider.bounds.size.x + BrickSpawner.BrickGap.x)),
                y = BrickSpawner.SpawnReferencePosition.y + (brickSpawn.LevelOffset * (brickInstance.BrickCollider.bounds.size.y + BrickSpawner.BrickGap.y)),
                z = ZPosition
            };
            brickInstance.transform.position = spawnOffset;
        }
        OnGameInitialize?.Invoke(this, new GameInitializeEventArgs());
    }

    private void StartGame()
    {
        OnGameStart?.Invoke(this, new GameStartEventArgs());
    }
}
