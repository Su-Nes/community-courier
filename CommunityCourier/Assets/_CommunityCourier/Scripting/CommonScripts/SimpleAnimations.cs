using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimations : MonoBehaviour
{
    [SerializeField] private Transform targetTf;
    [SerializeField] private RotationAxis rotationAxis;
    [SerializeField] private float heightBobAmplitude, bobSpeed, rotationSpeed;
    private Vector3 startOffset;

    private enum RotationAxis
    {
        X,
        Y,
        Z
    }

    private void Start()
    {
        startOffset = targetTf.localPosition;
    }

    private void Update()
    {
        HandleBob();
        HandleRotation();
    }

    private void HandleBob()
    {
        targetTf.localPosition = startOffset + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * heightBobAmplitude;
    }

    private void HandleRotation()
    {
        switch (rotationAxis)
        {
            case RotationAxis.X:
                targetTf.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
                break;
            
            case RotationAxis.Y:
                targetTf.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                break;
            
            case RotationAxis.Z:
                targetTf.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                break;
        }
    }
}
