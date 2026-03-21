using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public class CharacterCreator : NetworkBehaviour
{
    [SerializeField] private PlayerController playerObject;
    [SerializeField] private CharacterParts characterParts;
    
    [Serializable] private class CharacterParts
    {
        [SerializeField] private GameObject[] bodies;
        [SerializeField] private GameObject[] legs;
        [SerializeField] private float shortLegLength, normalLegLength, longLegLength;
        [SerializeField] private GameObject[] bags;
        [SerializeField] private GameObject[] leftEyes;
        [SerializeField] private GameObject[] rightEyes;
        [SerializeField] private GameObject[] mouths;
        [SerializeField] private Material mainMaterial;
        [SerializeField] private Material bagMaterial;
        [SerializeField] private Material altMaterial;
    }
    
    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
    }

    private SyncVar<int> bodyIndex = new();
    private SyncVar<int> legIndex = new();
    private SyncVar<int> bagIndex = new();
    private SyncVar<int> leftEyeIndex = new();
    private SyncVar<int> rightEyeIndex = new();
    private SyncVar<int> mouthIndex = new();

    private void BuildCharacter()
    {
        
    }
}
