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
        public Material[] bodyMaterials;
        public Material[] bagMaterials;
        public Material[] accentMaterials;
    }
    
    private SyncVar<int> bodyIndex = new();
    private SyncVar<int> legIndex = new();
    private SyncVar<int> bagIndex = new();
    private SyncVar<int> leftEyeIndex = new();
    private SyncVar<int> rightEyeIndex = new();
    private SyncVar<int> mouthIndex = new();
    private SyncVar<int> bodyMaterialIndex = new();
    private SyncVar<int> bagMaterialIndex = new();
    private SyncVar<int> accentMaterialIndex = new();
    
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
        bodyMaterialIndex.value = Random.Range(0, characterParts.bodyMaterials.Length);
        bagMaterialIndex.value = Random.Range(0, characterParts.bagMaterials.Length);
        accentMaterialIndex.value = Random.Range(0, characterParts.accentMaterials.Length);
        
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
        characterBody.GetComponent<Renderer>().material = characterParts.bodyMaterials[bodyMaterialIndex];
        
        // instantiate each body part on the body pivot points
        Renderer[] legRenderer = Instantiate(characterParts.legs[legIndex], characterBody.Find("Pivot_Legs")).transform.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in legRenderer) // assign materials to instantiated object
            r.material = characterParts.accentMaterials[accentMaterialIndex];
        
        Renderer bagRenderer = Instantiate(characterParts.bags[bagIndex], characterBody.Find("Pivot_Bag")).GetComponent<Renderer>();
        if (bagRenderer.transform.childCount > 0)
        {
            foreach (Renderer r in bagRenderer.transform.GetComponentsInChildren<Renderer>())
                r.material = characterParts.bagMaterials[bagMaterialIndex];
        }else
            bagRenderer.material = characterParts.bagMaterials[bagMaterialIndex];
        
        Renderer leftEyeRenderer = Instantiate(characterParts.leftEyes[leftEyeIndex], characterBody.Find("Pivot_EyeL")).GetComponent<Renderer>();
        if (leftEyeRenderer.transform.childCount > 0)
            leftEyeRenderer.material = characterParts.accentMaterials[accentMaterialIndex];
        
        Renderer rightEyeRenderer = Instantiate(characterParts.rightEyes[rightEyeIndex], characterBody.Find("Pivot_EyeR")).GetComponent<Renderer>();
        if (rightEyeRenderer.transform.childCount > 0)
            rightEyeRenderer.material = characterParts.accentMaterials[accentMaterialIndex];
        
        Renderer mouthRenderer = Instantiate(characterParts.mouths[mouthIndex], characterBody.Find("Pivot_Mouth")).GetComponent<Renderer>();
        if (mouthRenderer.materials.Length > 1) // this is only for the tongue mouth right now
            mouthRenderer.materials[1] = characterParts.accentMaterials[accentMaterialIndex];
        
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
