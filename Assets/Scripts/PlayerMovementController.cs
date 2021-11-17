using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerMovementController : NetworkBehaviour
{
    public float MovementScale = 1.2F;

    private new Rigidbody rigidbody;
    private Vector3 movement;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        movement = new Vector3();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Camera.main.projectionMatrix);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        rigidbody.MovePosition(transform.TransformPoint(movement * Time.deltaTime * MovementScale));
    }
}
