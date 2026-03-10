using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float lerpValue = .2f;
    
    private void Update()
    {
        if(target != null)
            LookAtPosition(target.position);
    }

    public void LookAtPosition(Vector3 position)
    {
        Vector3 lookRot = transform.position + position;
        lookRot.y = transform.position.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookRot), lerpValue);
    }
}
