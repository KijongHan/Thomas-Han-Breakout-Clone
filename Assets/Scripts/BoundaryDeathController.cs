using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDeathEventArgs : EventArgs {}

public class BoundaryDeathController : MonoBehaviour
{
    public static event EventHandler<BoundaryDeathEventArgs> OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<BallController>(out _))
        {
            OnDeath?.Invoke(this, new BoundaryDeathEventArgs());
        }
    }
}
