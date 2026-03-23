using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public Material[] mainMaterials;
        public Material[] bagMaterials;
        public Material[] altMaterials;
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
        
        CreateRandomCharacter();
    }

    public void CreateRandomCharacter()
    {
        bodyIndex.value = Random.Range(0, characterParts.bodies.Length);
        legIndex.value = Random.Range(0, characterParts.legs.Length);
        bagIndex.value = Random.Range(0, characterParts.bags.Length);
        leftEyeIndex.value = Random.Range(0, characterParts.leftEyes.Length);
        rightEyeIndex.value = Random.Range(0, characterParts.rightEyes.Length);
        mouthIndex.value = Random.Range(0, characterParts.mouths.Length);
        
        BuildCharacter();
    }

    private void BuildCharacter()
    {
        // destroy all children under player body transform. yea destroying everything every time one part changes is bad optimization but here it hopefully shouldn't matter
        foreach (Transform child in playerObject.BodyTransform)
        {
            Destroy(child.gameObject);
        }
        
        // instantiate body of character
        Transform characterBody = Instantiate(characterParts.bodies[bodyIndex], playerObject.BodyTransform).transform;
        
        // instantiate each body part on the body pivot points
        Renderer[] legRenderer = Instantiate(characterParts.legs[legIndex], characterBody.Find("Pivot_Legs")).transform.GetComponentsInChildren<Renderer>();
        Renderer bagRenderer = Instantiate(characterParts.bags[bagIndex], characterBody.Find("Pivot_Bag")).GetComponent<Renderer>();
        Renderer leftEyeRenderer = Instantiate(characterParts.leftEyes[leftEyeIndex], characterBody.Find("Pivot_EyeL")).GetComponent<Renderer>();
        Renderer rightEyeRenderer = Instantiate(characterParts.rightEyes[rightEyeIndex], characterBody.Find("Pivot_EyeR")).GetComponent<Renderer>();
        Renderer mouthRenderer = Instantiate(characterParts.mouths[mouthIndex], characterBody.Find("Pivot_Mouth")).GetComponent<Renderer>();
        
        // depending on leg index move the character body up so feet are on the ground
        characterBody.position = characterBody.Find("Pivot_Legs").position;
        switch (legIndex) // this shit works bad but I can't be bothered rn
        {
            case 0:
                characterBody.Translate(-characterBody.up * characterParts.shortLegLength);
                break;
            
            case 1:
                characterBody.Translate(-characterBody.up * characterParts.normalLegLength);
                break;
            
            case 2:
                characterBody.Translate(-characterBody.up * characterParts.longLegLength);
                break;
        }
    }
}
