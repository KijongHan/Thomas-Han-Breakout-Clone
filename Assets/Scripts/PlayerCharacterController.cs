using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerScoreUpdated : EventArgs
{
    public int ScoreFrom { get; set; }
    public int ScoreTo { get; set; }
}

public class PlayerBallInitialized : EventArgs
{
    public PlayerCharacterController Player { get; set; }
    public BallController InitializedBall { get; set; }
}

public class PlayerCharacterController : NetworkBehaviour
{
    public static event EventHandler<PlayerScoreUpdated> OnPlayerScoreUpdated;
    public static event EventHandler<PlayerBallInitialized> OnPlayerBallInitialized;

    public BallController BallPrefab;

    public Collider Collider { get; private set; }
    [SyncVar(hook = nameof(OnPlayerScoreUpdatedHook))]
    public int CurrentScore = 0;

    void OnEnable()
    {
        OnDisable();
        BoundaryDeathController.OnDeath += HandleOnDeath;
        GameController.OnGameInitialize += HandleOnGameInitialize;
        BrickController.OnBrickDestroyed += HandleOnBrickDestroyed;
        OnPlayerBallInitialized += HandleOnPlayerBallInitialized;
    }

    void OnDisable()
    {
        BoundaryDeathController.OnDeath -= HandleOnDeath;
        GameController.OnGameInitialize -= HandleOnGameInitialize;
        BrickController.OnBrickDestroyed -= HandleOnBrickDestroyed;
        OnPlayerBallInitialized -= HandleOnPlayerBallInitialized;
    }

    private void HandleOnPlayerBallInitialized(object sender, PlayerBallInitialized e)
    {
        if (netId != e.Player.netId)
        {
            Physics.IgnoreCollision(e.InitializedBall.Collider, Collider);
        }
    }

    private void HandleOnBrickDestroyed(object sender, BrickDestroyedEventArgs e)
    {
        var prevScore = CurrentScore;
        CurrentScore += e.Brick.ScorePerDestroyedBrick;
        Debug.Log($"Current Score: {CurrentScore}");
    }

    private void OnPlayerScoreUpdatedHook(int oldScore, int newScore)
    {
        if (!isLocalPlayer) return;
        OnPlayerScoreUpdated?.Invoke(this, new PlayerScoreUpdated { ScoreFrom = oldScore, ScoreTo = newScore });
    }

    private void HandleOnGameInitialize(object sender, GameInitializeEventArgs e)
    {
        CurrentScore = 0;
    }

    private void HandleOnDeath(object sender, BoundaryDeathEventArgs e)
    {
        if (e.DeadBall.PlayerId.netId != this.netIdentity.netId) return;
        InitializeBall();
    }

    [Command]
    private void InitializeBallCmd()
    {
        InitializeBall();
    }

    private void InitializeBall()
    {
        var ballInstance = Instantiate(BallPrefab);
        NetworkServer.Spawn(ballInstance.gameObject);
        ballInstance.PlayerId = netIdentity;
        ballInstance.FollowTransform = transform;
        ballInstance.transform.position = new Vector3
        {
            x = transform.position.x,
            y = transform.position.y + (ballInstance.Collider.bounds.size.y / 2) + 0.025F,
            z = transform.position.z
        };
        ballInstance.FollowPlayerOffset = ballInstance.transform.position - transform.position;
        OnPlayerBallInitialized?.Invoke(this, new PlayerBallInitialized { InitializedBall = ballInstance, Player = this });
    }

    private void Awake()
    {
        Collider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer) return;
        InitializeBallCmd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
