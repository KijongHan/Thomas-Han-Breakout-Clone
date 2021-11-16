using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDeathEventArgs : EventArgs 
{
    public BallController DeadBall { get; set; }
}

public class BoundaryDeathController : MonoBehaviour
{
    public static event EventHandler<BoundaryDeathEventArgs> OnDeath;

    private new Collider collider;

    void Awake()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        OnDisable();
        GameController.OnGameStart += HandleOnGameStart;
    }

    void OnDisable()
    {
        GameController.OnGameStart -= HandleOnGameStart;
    }

    private void HandleOnGameStart(object sender, GameStartEventArgs e)
    {
        collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallController ball))
        {
            OnDeath?.Invoke(this, new BoundaryDeathEventArgs { DeadBall = ball });
        }
    }
}
