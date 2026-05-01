using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CharacterCreator : NetworkBehaviour
{
    [SerializeField] private PlayerController playerObject;
    private CharacterBuilder characterBuilder;
    
    public PlayerController PlayerObject => playerObject;
    
    private SyncVar<int> bodyIndex = new(0, ownerAuth: true);
    private SyncVar<int> legIndex = new(0, ownerAuth: true);
    private SyncVar<int> bagIndex = new(0, ownerAuth: true);
    private SyncVar<int> leftEyeIndex = new(0, ownerAuth: true);
    private SyncVar<int> rightEyeIndex = new(0, ownerAuth: true);
    private SyncVar<int> mouthIndex = new(0, ownerAuth: true);
    private SyncVar<int> bodyMaterialIndex = new(0, ownerAuth: true);
    private SyncVar<int> bagMaterialIndex = new(0, ownerAuth: true);
    private SyncVar<int> accentMaterialIndex = new(0, ownerAuth: true);

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
        
        characterBuilder = playerObject.BodyTransform.GetComponent<CharacterBuilder>();
        BuildAllOtherPlayerCharacters();

        if (!isOwner)
            return;
        
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
        BuildAllOtherPlayerCharacters();
    }

    private void InitiateButton(Parts part, TMP_Text buttonText)
    {
        switch (part)
        {
            case Parts.Body:
                buttonText.text = "Body: " + characterBuilder.bodies[bodyIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Legs:
                buttonText.text = "Legs: " + characterBuilder.legs[legIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Bag:
                buttonText.text = "Bag: " + characterBuilder.bags[bagIndex.value].name.Split('_')[1];
                break;
            
            case Parts.EyeL:
                buttonText.text = "Eye L: " + characterBuilder.leftEyes[leftEyeIndex.value].name.Split('_')[1];
                break;
            
            case Parts.EyeR:
                buttonText.text = "Eye R: " + characterBuilder.rightEyes[rightEyeIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Mouth:
                buttonText.text = "Mouth: " + characterBuilder.mouths[mouthIndex.value].name.Split('_')[1];
                break;
            
            case Parts.BodyMaterial:
                buttonText.text = "Body colour: " + characterBuilder.bodyMaterials[bodyMaterialIndex.value].name.Split('_')[1];
                break;
            
            case Parts.BagMaterial:
                buttonText.text = "Bag colour: " + characterBuilder.bagMaterials[bagMaterialIndex.value].name.Split('_')[1];
                break;
            
            case Parts.AccentMaterial:
                buttonText.text = "Accent colour: " + characterBuilder.accentMaterials[accentMaterialIndex.value].name.Split('_')[1];
                break;
        }
        
        CallForBuildCharacter(owner.GetValueOrDefault());
        BuildAllOtherPlayerCharacters();
    }
    
    
    public void CycleBodyPart(Parts part)
    {
        switch (part)
        {
            case Parts.Body:
                bodyIndex.value++;
                if (bodyIndex.value >= characterBuilder.bodies.Length)
                    bodyIndex.value = 0;
                
                bodyButton.transform.GetComponentInChildren<TMP_Text>().text = "Body: " + characterBuilder.bodies[bodyIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Legs:
                legIndex.value++;
                if (legIndex.value >= characterBuilder.legs.Length)
                    legIndex.value = 0;
                
                legButton.transform.GetComponentInChildren<TMP_Text>().text = "Legs: " + characterBuilder.legs[legIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Bag:
                bagIndex.value++;
                if (bagIndex.value >= characterBuilder.bags.Length)
                    bagIndex.value = 0;
                
                bagButton.transform.GetComponentInChildren<TMP_Text>().text = "Bag: " + characterBuilder.bags[bagIndex.value].name.Split('_')[1];
                break;
            
            case Parts.EyeL:
                leftEyeIndex.value++;
                if (leftEyeIndex.value >= characterBuilder.leftEyes.Length)
                    leftEyeIndex.value = 0;

                eyeLButton.transform.GetComponentInChildren<TMP_Text>().text = "Eye L: " + characterBuilder.leftEyes[leftEyeIndex.value].name.Split('_')[1];
                break;
            
            case Parts.EyeR:
                rightEyeIndex.value++;
                if (rightEyeIndex.value >= characterBuilder.rightEyes.Length)
                    rightEyeIndex.value = 0;

                eyeRButton.transform.GetComponentInChildren<TMP_Text>().text = "Eye R: " + characterBuilder.rightEyes[rightEyeIndex.value].name.Split('_')[1];
                break;
            
            case Parts.Mouth:
                mouthIndex.value++;
                if (mouthIndex.value >= characterBuilder.mouths.Length)
                    mouthIndex.value = 0;

                mouthButton.transform.GetComponentInChildren<TMP_Text>().text = "Mouth: " + characterBuilder.mouths[mouthIndex.value].name.Split('_')[1];
                break;
            
            case Parts.BodyMaterial:
                bodyMaterialIndex.value++;
                if (bodyMaterialIndex.value >= characterBuilder.bodyMaterials.Length)
                    bodyMaterialIndex.value = 0;
                break;
            
            case Parts.BagMaterial:
                bagMaterialIndex.value++;
                if (bagMaterialIndex.value >= characterBuilder.bagMaterials.Length)
                    bagMaterialIndex.value = 0;
                break;
            
            case Parts.AccentMaterial:
                accentMaterialIndex.value++;
                if (accentMaterialIndex.value >= characterBuilder.accentMaterials.Length)
                    accentMaterialIndex.value = 0;
                break;
        }

        CallForBuildCharacter(owner.GetValueOrDefault());
        BuildAllOtherPlayerCharacters();
    }

    public void CreateRandomCharacter()
    {
        bodyIndex.value = Random.Range(0, characterBuilder.bodies.Length);
        legIndex.value = Random.Range(0, characterBuilder.legs.Length);
        bagIndex.value = Random.Range(0, characterBuilder.bags.Length);
        leftEyeIndex.value = Random.Range(0, characterBuilder.leftEyes.Length);
        rightEyeIndex.value = Random.Range(0, characterBuilder.rightEyes.Length);
        mouthIndex.value = Random.Range(0, characterBuilder.mouths.Length);
        bodyMaterialIndex.value = Random.Range(0, characterBuilder.bodyMaterials.Length);
        bagMaterialIndex.value = Random.Range(0, characterBuilder.bagMaterials.Length);
        accentMaterialIndex.value = Random.Range(0, characterBuilder.accentMaterials.Length);
        
        InitiateButton(Parts.Body, bodyButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.Legs, legButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.Bag, bagButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.EyeL, eyeLButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.EyeR, eyeRButton.transform.GetComponentInChildren<TMP_Text>());
        InitiateButton(Parts.Mouth, mouthButton.transform.GetComponentInChildren<TMP_Text>());
        
        CallForBuildCharacter(owner.GetValueOrDefault());
        BuildAllOtherPlayerCharacters();
    }

    [TargetRpc]
    public void CallForBuildCharacter(PlayerID target)
    {
        characterBuilder.BuildCharacter(bodyIndex.value, legIndex.value, bagIndex.value, leftEyeIndex.value, rightEyeIndex.value, mouthIndex.value, bodyMaterialIndex.value, bagMaterialIndex.value, accentMaterialIndex.value);
    }

    private void BuildAllOtherPlayerCharacters()
    {
        foreach (var charCreator in FindObjectsOfType<CharacterCreator>())
        {
            if (charCreator.owner.GetValueOrDefault() != owner.GetValueOrDefault())
            {
                CallForBuildCharacter(charCreator.owner.GetValueOrDefault());
            }
        }
    }
    
    public void FinishCharacterCreation()
    {
        BuildAllOtherPlayerCharacters();
        transform.parent.GetComponent<CharacterCreationState>().CharacterComplete();
    }
}