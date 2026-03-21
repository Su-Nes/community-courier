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
        public GameObject[] bodies;
        public GameObject[] legs;
        public float shortLegLength, normalLegLength, longLegLength;
        public GameObject[] bags;
        public GameObject[] leftEyes;
        public GameObject[] rightEyes;
        public GameObject[] mouths;
        public Material mainMaterial;
        public Material bagMaterial;
        public Material altMaterial;
    }
    
    private SyncVar<int> bodyIndex = new();
    private SyncVar<int> legIndex = new();
    private SyncVar<int> bagIndex = new();
    private SyncVar<int> leftEyeIndex = new();
    private SyncVar<int> rightEyeIndex = new();
    private SyncVar<int> mouthIndex = new();
    
    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
        
        BuildCharacter();
    }

    private void BuildCharacter()
    {
        // destroy all children under player body transform
        foreach (Transform child in playerObject.BodyTransform)
        {
            Destroy(child.gameObject);
        }
        
        // instantiate body of character
        Transform characterBody = Instantiate(characterParts.bodies[bodyIndex], playerObject.BodyTransform).transform;
        
        // depending on leg index move the character body up so feet are on the ground
        switch (legIndex)
        {
            case 0:
                characterBody.Translate(Vector3.up * characterParts.shortLegLength);
                break;
            
            case 1:
                characterBody.Translate(Vector3.up * characterParts.normalLegLength);
                break;
            
            case 2:
                characterBody.Translate(Vector3.up * characterParts.longLegLength);
                break;
        }
        
        // instantiate each body part
        Instantiate(characterParts.legs[legIndex], characterBody.Find("Pivot_Legs"));
        Instantiate(characterParts.bags[bagIndex], characterBody.Find("Pivot_Bag"));
        Instantiate(characterParts.leftEyes[leftEyeIndex], characterBody.Find("Pivot_EyeL"));
        Instantiate(characterParts.rightEyes[rightEyeIndex], characterBody.Find("Pivot_EyeR"));
        Instantiate(characterParts.mouths[mouthIndex], characterBody.Find("Pivot_Mouth"));
    }
}
