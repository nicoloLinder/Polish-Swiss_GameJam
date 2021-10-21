using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float TargetHeight { get; set; }

    private void Start()
    {
        TargetHeight = transform.position.y;
    }

    private void Update()
    {
        var position = transform.position;
        float lerpedHeight = Mathf.Lerp(position.y, TargetHeight, 0.1f);
        
        position = new Vector3(position.x, lerpedHeight, -10);
        transform.position = position;
    }
}
