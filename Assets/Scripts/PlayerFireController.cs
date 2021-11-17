using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class FireEventArgs : EventArgs 
{
    public float InitialFireAngle { get; set; }
    public NetworkIdentity PlayerId { get; set; }
}

public class PlayerFireController : NetworkBehaviour
{
    public static event EventHandler<FireEventArgs> OnFire;

    public float InitialFireAngle = 45F;

    public PlayerCharacterController Player { get; private set; }

    void Awake()
    {
        Player = GetComponent<PlayerCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire(Player.netIdentity);
        }
    }

    [Command]
    void Fire(NetworkIdentity playerId)
    {
        OnFire?.Invoke(this, new FireEventArgs { PlayerId = playerId, InitialFireAngle = InitialFireAngle });
    }
}
