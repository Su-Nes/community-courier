using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float animSpeed, bodyVerticalAmplitude, bagVerticalAmplitude, bagSwayOffset, legRotationAmplitude;
    
    private Transform body, bag, legPivot, legL, legR, eyeL, eyeR;
    
    
}
