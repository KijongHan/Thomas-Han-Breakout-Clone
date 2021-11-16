using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireEventArgs : EventArgs 
{
    public PlayerFireController PlayerFire { get; set; }
}

public class PlayerFireController : MonoBehaviour
{
    public static event EventHandler<FireEventArgs> OnFire;

    public float InitialFireAngle = 45F;

    public PlayerController Player { get; private set; }

    void Awake()
    {
        Player = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnFire?.Invoke(this, new FireEventArgs { PlayerFire = this });
        }
    }
}
