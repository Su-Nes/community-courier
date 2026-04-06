using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public class CharacterAnimator : NetworkBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float animSpeed, bodyVerticalAmplitude, bagVerticalAmplitude, bagSwayOffset, legRotationAmplitude, legRotationOffset, blinkDuration, lerpToIdleValue;
    [SerializeField] private Vector2 blinkRate;
    private SyncVar<float> t = new (ownerAuth: true), startZL = new (ownerAuth: true), startZR = new (ownerAuth: true), blinkTimer = new (ownerAuth: true);
    
    private SyncVar<Transform> body = new (ownerAuth: true), bagPivot = new (ownerAuth: true), legPivot = new (ownerAuth: true), legL = new (ownerAuth: true), legR = new (ownerAuth: true), eyeL = new (ownerAuth: true), eyeR = new (ownerAuth: true);
    
    public void AssignBodyParts()
    {
        body.value = transform.GetChild(0);
        bagPivot.value = body.value.Find("Pivot_Bag");
        legPivot.value = body.value.Find("Pivot_Legs");
        legL.value = legPivot.value.GetChild(0).Find("LegL");
        legR.value = legPivot.value.GetChild(0).Find("LegR");
        eyeL.value = body.value.Find("Pivot_EyeL").GetChild(0).childCount switch // get iris of eye if it has a brow
        {
            0 => body.value.Find("Pivot_EyeL").GetChild(0),
            1 => body.value.Find("Pivot_EyeL").GetChild(0).GetChild(0),
            _ => eyeL
        };
        eyeR.value = body.value.Find("Pivot_EyeR").GetChild(0).childCount switch
        {
            0 => body.value.Find("Pivot_EyeR").GetChild(0),
            1 => body.value.Find("Pivot_EyeR").GetChild(0).GetChild(0),
            _ => eyeR
        };

        startZL.value = eyeL.value.localScale.z;
        startZR.value = eyeR.value.localScale.z;
        RandomizeBlinkTimer();
    }

    private void Update()
    {
        if (body == null)
            return;

        if (playerController.MoveDirection.magnitude > 0f)
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
        if (legL.value == null || legL.value == null)
            return;
        
        float rotation = Mathf.Cos(t) * legRotationAmplitude;
        legL.value.transform.localRotation = Quaternion.AngleAxis(rotation + legRotationOffset, Vector3.right);
        legR.value.transform.localRotation = Quaternion.AngleAxis(-rotation + legRotationOffset, Vector3.right);
    }
    
    private void IsMoving()
    {
        t.value += Time.deltaTime * animSpeed;
        if (t >= 2f * Mathf.PI)
            t.value = 0f;
    }
    
    private void ReturnToIdle()
    {
        t.value = Mathf.Lerp(t, .5f * Mathf.PI, lerpToIdleValue);
    }
    
    private void HandleEyeBlinking()
    {
        if (blinkTimer > 0)
            blinkTimer.value -= Time.deltaTime;
        else
        {
            StartCoroutine(Blink());
            RandomizeBlinkTimer();
        }
    }
    
    private void RandomizeBlinkTimer()
    {
        blinkTimer.value = Random.Range(blinkRate.x, blinkRate.y);
    }
    
    private IEnumerator Blink()
    {
        if (eyeL.value == null || eyeR.value == null)
            yield break;
        
        eyeL.value.localScale = new Vector3(eyeL.value.localScale.x, eyeL.value.localScale.y, 0f);
        eyeR.value.localScale = new Vector3(eyeR.value.localScale.x, eyeR.value.localScale.y, 0f);
        yield return new WaitForSeconds(blinkDuration);
        eyeL.value.localScale = new Vector3(eyeL.value.localScale.x, eyeL.value.localScale.y, startZL);
        eyeR.value.localScale = new Vector3(eyeR.value.localScale.x, eyeR.value.localScale.y, startZR);
    }
}
