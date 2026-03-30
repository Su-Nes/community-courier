using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterCreator : NetworkBehaviour
{
    [SerializeField] private PlayerController playerObject;
    [SerializeField] private CharacterParts characterParts;
    
    public PlayerController PlayerObject => playerObject;
    
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

    public enum Parts
    {
        Body,
        Legs,
        Bag,
        EyeL,
        EyeR,
        Mouth,
        BodyMaterial,
        BagMaterial,
        AccentMaterial
    }

    [SerializeField] private Button bodyButton, legButton, bagButton, eyeLButton, eyeRButton, mouthButton, bodyMatButton, bagMatButton, accentMatButton;
    
    
    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
        
        bodyButton.onClick.AddListener(delegate {CycleBodyPart(Parts.Body);});
        InitiateButton(Parts.Body, bodyButton.transform.GetComponentInChildren<TMP_Text>());
        legButton.onClick.AddListener(delegate { CycleBodyPart(Parts.Legs);});
        InitiateButton(Parts.Legs, legButton.transform.GetComponentInChildren<TMP_Text>());
        bagButton.onClick.AddListener(delegate {CycleBodyPart(Parts.Bag);});
        InitiateButton(Parts.Bag, bagButton.transform.GetComponentInChildren<TMP_Text>());
        eyeLButton.onClick.AddListener(delegate {CycleBodyPart(Parts.EyeL);});
        InitiateButton(Parts.EyeL, eyeLButton.transform.GetComponentInChildren<TMP_Text>());
        eyeRButton.onClick.AddListener(delegate {CycleBodyPart(Parts.EyeR);});
        InitiateButton(Parts.EyeR, eyeRButton.transform.GetComponentInChildren<TMP_Text>());
        mouthButton.onClick.AddListener(delegate {CycleBodyPart(Parts.Mouth);});
        InitiateButton(Parts.Mouth, mouthButton.transform.GetComponentInChildren<TMP_Text>());
        
        bodyMatButton.onClick.AddListener(delegate {CycleBodyPart(Parts.BodyMaterial);});
        bagMatButton.onClick.AddListener(delegate {CycleBodyPart(Parts.BagMaterial);});
        accentMatButton.onClick.AddListener(delegate {CycleBodyPart(Parts.AccentMaterial);});
        
        CreateRandomCharacter();
    }

    private void InitiateButton(Parts part, TMP_Text buttonText)
    {
        switch (part)
        {
            case Parts.Body:
                buttonText.text = "Body: " + characterParts.bodies[bodyIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Legs:
                buttonText.text = "Legs: " + characterParts.legs[legIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Bag:
                buttonText.text = "Bag: " + characterParts.bags[bagIndex.value].name.Split('_')[1];
                break;
            
            case Parts.EyeL:
                buttonText.text = "Eye L: " + characterParts.leftEyes[leftEyeIndex.value].name.Split('_')[1];
                break;
            
            case Parts.EyeR:
                buttonText.text = "Eye R: " + characterParts.rightEyes[rightEyeIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Mouth:
                buttonText.text = "Mouth: " + characterParts.mouths[mouthIndex.value].name.Split('_')[1];
                break;
            
            case Parts.BodyMaterial:
                buttonText.text = "Body colour: " + characterParts.bodyMaterials[bodyMaterialIndex.value].name.Split('_')[1];
                break;
            
            case Parts.BagMaterial:
                buttonText.text = "Bag colour: " + characterParts.bagMaterials[bagMaterialIndex.value].name.Split('_')[1];
                break;
            
            case Parts.AccentMaterial:
                buttonText.text = "Accent colour: " + characterParts.accentMaterials[accentMaterialIndex.value].name.Split('_')[1];
                break;
        }
        
        BuildCharacter();
    }
    

    [ServerRpc(requireOwnership:false)]
    public void CycleBodyPart(Parts part)
    {
        switch (part)
        {
            case Parts.Body:
                bodyIndex.value++;
                if (bodyIndex.value >= characterParts.bodies.Length)
                    bodyIndex.value = 0;
                
                bodyButton.transform.GetComponentInChildren<TMP_Text>().text = "Body: " + characterParts.bodies[bodyIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Legs:
                legIndex.value++;
                if (legIndex.value >= characterParts.legs.Length)
                    legIndex.value = 0;
                
                legButton.transform.GetComponentInChildren<TMP_Text>().text = "Legs: " + characterParts.legs[legIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Bag:
                bagIndex.value++;
                if (bagIndex.value >= characterParts.bags.Length)
                    bagIndex.value = 0;
                
                bagButton.transform.GetComponentInChildren<TMP_Text>().text = "Bag: " + characterParts.bags[bagIndex.value].name.Split('_')[1];
                break;
            
            case Parts.EyeL:
                leftEyeIndex.value++;
                if (leftEyeIndex.value >= characterParts.leftEyes.Length)
                    leftEyeIndex.value = 0;

                eyeLButton.transform.GetComponentInChildren<TMP_Text>().text = "Eye L: " + characterParts.leftEyes[leftEyeIndex.value].name.Split('_')[1];
                break;
            
            case Parts.EyeR:
                rightEyeIndex.value++;
                if (rightEyeIndex.value >= characterParts.rightEyes.Length)
                    rightEyeIndex.value = 0;

                eyeRButton.transform.GetComponentInChildren<TMP_Text>().text = "Eye R: " + characterParts.rightEyes[rightEyeIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Mouth:
                mouthIndex.value++;
                if (mouthIndex.value >= characterParts.mouths.Length)
                    mouthIndex.value = 0;

                mouthButton.transform.GetComponentInChildren<TMP_Text>().text = "Mouth: " + characterParts.mouths[mouthIndex.value].name.Split('_')[1];
                break;
            
            case Parts.BodyMaterial:
                bodyMaterialIndex.value++;
                if (bodyMaterialIndex.value >= characterParts.bodyMaterials.Length)
                    bodyMaterialIndex.value = 0;
                break;
            
            case Parts.BagMaterial:
                bagMaterialIndex.value++;
                if (bagMaterialIndex.value >= characterParts.bagMaterials.Length)
                    bagMaterialIndex.value = 0;
                break;
            
            case Parts.AccentMaterial:
                accentMaterialIndex.value++;
                if (accentMaterialIndex.value >= characterParts.accentMaterials.Length)
                    accentMaterialIndex.value = 0;
                break;
        }
        
        BuildCharacter();
    }

    [ServerRpc(requireOwnership:false)]
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
        
        InitiateButton(Parts.Body, bodyButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.Legs, legButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.Bag, bagButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.EyeL, eyeLButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.EyeR, eyeRButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.Mouth, mouthButton.transform.GetComponentInChildren<TMP_Text>());
        
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
        else if (leftEyeRenderer.materials.Length > 1)
        {
            Material[] materials = new Material[characterParts.accentMaterials.Length];
            materials[0] = leftEyeRenderer.materials[0];
            materials[1] = characterParts.accentMaterials[accentMaterialIndex];
            
            leftEyeRenderer.materials = materials;
        }
        
        Renderer rightEyeRenderer = Instantiate(characterParts.rightEyes[rightEyeIndex], characterBody.Find("Pivot_EyeR")).GetComponent<Renderer>();
        if (rightEyeRenderer.transform.childCount > 0)
            rightEyeRenderer.material = characterParts.accentMaterials[accentMaterialIndex];
        else if (rightEyeRenderer.materials.Length > 1)
        {
            Material[] materials = new Material[characterParts.accentMaterials.Length];
            materials[0] = rightEyeRenderer.materials[0];
            materials[1] = characterParts.accentMaterials[accentMaterialIndex];
            
            rightEyeRenderer.materials = materials;

        }
        
        Renderer mouthRenderer = Instantiate(characterParts.mouths[mouthIndex], characterBody.Find("Pivot_Mouth")).GetComponent<Renderer>();
        if (mouthRenderer.materials.Length > 1) // this is only for the tongue mouth right now
        {
            Material[] materials = new Material[characterParts.accentMaterials.Length];
            materials[0] = mouthRenderer.materials[0];
            materials[1] = characterParts.accentMaterials[accentMaterialIndex];
            
            mouthRenderer.materials = materials;

        }
        
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

    public void FinishCharacterCreation()
    {
        transform.parent.GetComponent<CharacterCreationState>().CharacterComplete();
    }
}
