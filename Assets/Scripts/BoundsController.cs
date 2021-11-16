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

public class BoundsController : MonoBehaviour
{
    public Vector3 PositionScale;
    public Vector3 FaceDirection;
    public float Thickness = 1F;
    public float ZPosition = 0F;

    private BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var cameraOrigin = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        var cameraScreenCenterX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
        var cameraScreenCenterY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
        var screenSize = new Vector2
        {
            x = Vector2.Distance(cameraOrigin, cameraScreenCenterX) * 0.5F,
            y = Vector2.Distance(cameraOrigin, cameraScreenCenterY) * 0.5F
        };

        boxCollider.size = new Vector3(Thickness, screenSize.x * 2, Thickness);
        var cameraPosition = Camera.main.transform.position;
        transform.position = new Vector3
        {
            x = cameraPosition.x + (screenSize.x * PositionScale.x),
            y = cameraPosition.y + (screenSize.y * PositionScale.y),
            z = ZPosition
        };
        transform.LookAt(transform.parent.position, Vector3.up);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
