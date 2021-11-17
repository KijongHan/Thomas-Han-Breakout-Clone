using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreUpdated : EventArgs
{
    public int ScoreFrom { get; set; }
    public int ScoreTo { get; set; }
}

public class PlayerCharacterController : MonoBehaviour
{
    public static event EventHandler<PlayerScoreUpdated> OnPlayerScoreUpdated;

    public BallController BallPrefab;

    public int CurrentScore { get; private set; }

    void OnEnable()
    {
        OnDisable();
        BoundaryDeathController.OnDeath += HandleOnDeath;
        GameController.OnGameInitialize += HandleOnGameInitialize;
        BrickController.OnBrickDestroyed += HandleOnBrickDestroyed;
    }

    void OnDisable()
    {
        BoundaryDeathController.OnDeath -= HandleOnDeath;
        GameController.OnGameInitialize -= HandleOnGameInitialize;
        BrickController.OnBrickDestroyed -= HandleOnBrickDestroyed;
    }

    private void HandleOnBrickDestroyed(object sender, BrickDestroyedEventArgs e)
    {
        if (e.Ball.Player == this)
        {
            var prevScore = CurrentScore;
            CurrentScore += e.Brick.ScorePerDestroyedBrick;
            OnPlayerScoreUpdated?.Invoke(this, new PlayerScoreUpdated { ScoreFrom = prevScore, ScoreTo = CurrentScore });
            Debug.Log($"Current Score: {CurrentScore}");
        }
    }

    private void HandleOnGameInitialize(object sender, GameInitializeEventArgs e)
    {
        CurrentScore = 0;
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
