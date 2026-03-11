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
            LookAtTarget();
    }

    public void LookAtPosition(Vector3 position)
    {
        Vector3 lookRot = transform.position + position;
        lookRot.y = transform.position.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookRot), lerpValue);
    }

    private void LookAtTarget()
    {
        /*Vector3 lookRot = transform.position + target.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookRot), lerpValue);*/
        transform.LookAt(target);
    }
}
