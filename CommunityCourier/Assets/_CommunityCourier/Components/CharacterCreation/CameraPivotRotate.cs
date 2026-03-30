using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public class CameraPivotRotate : NetworkBehaviour
{
    [SerializeField] private float sensitivity, smoothingLerp, maxLookAngle;
    
    private float verticalRotation, horizontalRotation;
    
    private Quaternion targetRotation;


    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
    }

    public void HandleRotation()
    {
        float mouseX = InputManager.instance.inputActionAsset.FindAction("Look").ReadValue<Vector2>().x * sensitivity;
        float mouseY = InputManager.instance.inputActionAsset.FindAction("Look").ReadValue<Vector2>().y * sensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        
        horizontalRotation += mouseX;

        targetRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothingLerp * Time.deltaTime);
    }
}
