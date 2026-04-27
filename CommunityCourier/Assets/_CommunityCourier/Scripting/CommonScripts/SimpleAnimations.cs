using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimations : MonoBehaviour
{
    [SerializeField] private Transform targetTf;
    [SerializeField] private float heightBobAmplitude, bobSpeed, rotationSpeed;
    private Vector3 startOffset;

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
        startOffset.y += Mathf.Sin(Time.time * bobSpeed) * heightBobAmplitude;
        targetTf.localPosition = startOffset;
    }

    private void HandleRotation()
    {
        targetTf.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
