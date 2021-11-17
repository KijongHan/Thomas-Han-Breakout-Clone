using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHitEventArgs : EventArgs
{ 
    public BallController Ball { get; set; }
}

public class BrickDestroyedEventArgs : EventArgs
{
    public BrickController Brick { get; set; }
    public BallController Ball { get; set; }
}

public class BrickController : MonoBehaviour
{
    public static event EventHandler<BrickHitEventArgs> OnBrickHit;
    public static event EventHandler<BrickDestroyedEventArgs> OnBrickDestroyed;

    public int ScorePerDestroyedBrick = 100;
    public int Health = 1;

    public Collider BrickCollider { get; private set; }

    void Awake()
    {
        BrickCollider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out BallController ball))
        {
            OnBrickHit?.Invoke(this, new BrickHitEventArgs { Ball = ball });
            Health -= 1;
            if (Health <= 0)
            {
                OnBrickDestroyed?.Invoke(this, new BrickDestroyedEventArgs { Brick = this, Ball = ball });
                Destroy(gameObject);
            }
        }
    }
}
