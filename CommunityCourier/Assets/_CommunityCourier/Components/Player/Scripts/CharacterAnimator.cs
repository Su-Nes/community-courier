using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public class CharacterAnimator : NetworkBehaviour
{
    [SerializeField] private float animSpeed, bodyVerticalAmplitude, bagVerticalAmplitude, bagSwayOffset, legRotationAmplitude, legRotationOffset, blinkDuration, lerpToIdleValue;
    [SerializeField] private Vector2 blinkRate;

    private float t, startZL, startZR, blinkTimer;
    private Vector3 lastPosition;
    
    private Transform body, bagPivot, legPivot, legL, legR, eyeL, eyeR;
    
    public void AssignBodyParts()
    {
        body = transform.GetChild(0);
        bagPivot = body.Find("Pivot_Bag");
        legPivot = body.Find("Pivot_Legs");
        legL = legPivot.GetChild(0).Find("LegL");
        legR = legPivot.GetChild(0).Find("LegR");
        eyeL = body.Find("Pivot_EyeL").GetChild(0).childCount switch // get iris of eye if it has a brow
        {
            0 => body.Find("Pivot_EyeL").GetChild(0),
            1 => body.Find("Pivot_EyeL").GetChild(0).GetChild(0),
            _ => eyeL
        };
        eyeR = body.Find("Pivot_EyeR").GetChild(0).childCount switch
        {
            0 => body.Find("Pivot_EyeR").GetChild(0),
            1 => body.Find("Pivot_EyeR").GetChild(0).GetChild(0),
            _ => eyeR
        };

        startZL = eyeL.localScale.z;
        startZR = eyeR.localScale.z;
        RandomizeBlinkTimer();
    }

    private void Update()
    {
        if (body == null) // hihi
        {
            AssignBodyParts(); 
            return;
        }
        
        bool isMoving = transform.position != lastPosition;
        
        if (isMoving)
        {
            IsMoving();
        }
        else
            ReturnToIdle();
        
        HandleLegRotation();
        HandleEyeBlinking();
    }
    
    private void HandleLegRotation()
    {
        if (legL == null || legL == null)
            return;
        
        float rotation = Mathf.Cos(t) * legRotationAmplitude;
        legL.transform.localRotation = Quaternion.AngleAxis(rotation + legRotationOffset, Vector3.right);
        legR.transform.localRotation = Quaternion.AngleAxis(-rotation + legRotationOffset, Vector3.right);
    }
    
    private void IsMoving()
    {
        t += Time.deltaTime * animSpeed;
        if (t >= 2f * Mathf.PI)
            t = 0f;
        if (lastPosition != transform.position)
            lastPosition = transform.position;
    }
    
    private void ReturnToIdle()
    {
        t = Mathf.Lerp(t, .5f * Mathf.PI, lerpToIdleValue);
    }
    
    private void HandleEyeBlinking()
    {
        if (blinkTimer > 0)
            blinkTimer -= Time.deltaTime;
        else
        {
            StartCoroutine(Blink());
            RandomizeBlinkTimer();
        }
    }
    
    private void RandomizeBlinkTimer()
    {
        blinkTimer = Random.Range(blinkRate.x, blinkRate.y);
    }
    
    private IEnumerator Blink()
    {
        if (eyeL == null || eyeR == null)
            yield break;
        
        eyeL.localScale = new Vector3(eyeL.localScale.x, eyeL.localScale.y, 0f);
        eyeR.localScale = new Vector3(eyeR.localScale.x, eyeR.localScale.y, 0f);
        yield return new WaitForSeconds(blinkDuration);
        eyeL.localScale = new Vector3(eyeL.localScale.x, eyeL.localScale.y, startZL);
        eyeR.localScale = new Vector3(eyeR.localScale.x, eyeR.localScale.y, startZR);
    }
}
