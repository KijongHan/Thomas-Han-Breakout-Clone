using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
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
        rigidbody.MovePosition(transform.TransformPoint(movement * Time.deltaTime * MovementScale));
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("FIRE");
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("MOVE");
            movement = new Vector3(context.ReadValue<Vector2>().x, 0, 0);
            //movement *= 0.05F;
        }
        if (context.canceled)
        {
            Debug.Log("STOP MOVE");
            movement = new Vector3();
        }
    }
}
