using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] private Transform cameraTf;
    [SerializeField] private float collisionSensitivity = .25f;
    [SerializeField] private Vector3 camOffset;
    [SerializeField] private LayerMask layerMask;
    
    private RaycastHit hit;

    private void Start()
    {
        camOffset = cameraTf.localPosition;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.localRotation * camOffset, Color.red);
        
        if (Physics.Linecast(transform.position, transform.position + transform.localRotation * camOffset, out hit, layerMask))
        {
            cameraTf.localPosition = new Vector3(cameraTf.localPosition.x, cameraTf.localPosition.y, -Vector3.Distance(transform.position, hit.point) + collisionSensitivity);
        }
        else
        {
            cameraTf.localPosition = camOffset;
        }
    }
}
