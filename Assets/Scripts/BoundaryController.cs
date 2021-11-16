using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{ 
    Up,
    Down,
    Left,
    Right
}

public class BoundaryController : MonoBehaviour
{
    public Vector2 BoundaryScale;
    public Vector3 PositionScale;
    public Vector3 FaceDirection;
    public float Thickness = 1F;
    public float Depth = 4F;
    
    public float ZPosition { get; set; }

    private BoxCollider boxCollider;

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
        InitializeBoundary();
    }

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeBoundary()
    {
        var cameraOrigin = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        var cameraScreenCenterX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        var cameraScreenCenterY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        var screenSize = new Vector2
        {
            x = Vector2.Distance(cameraOrigin, cameraScreenCenterX) * 0.5F,
            y = Vector2.Distance(cameraOrigin, cameraScreenCenterY) * 0.5F
        };

        boxCollider.size = new Vector3
        {
            x = Depth,
            y = BoundaryScale.x * screenSize.x * 2 + BoundaryScale.y * screenSize.y * 2,
            z = Thickness
        };
        var cameraPosition = Camera.main.transform.position;
        transform.position = new Vector3
        {
            x = cameraPosition.x + (screenSize.x * PositionScale.x),
            y = cameraPosition.y + (screenSize.y * PositionScale.y),
            z = ZPosition
        };
        transform.LookAt(new Vector3(0F, 0F, ZPosition), Vector3.up);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, transform.rotation.eulerAngles.z);
    }
}
