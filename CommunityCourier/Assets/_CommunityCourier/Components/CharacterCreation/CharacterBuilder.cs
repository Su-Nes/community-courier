using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public class CharacterBuilder : NetworkBehaviour
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
    
    
    public void BuildCharacter(int bodyIndex, int legIndex, int bagIndex, int leftEyeIndex, int rightEyeIndex, int mouthIndex, int bodyMaterialIndex, int bagMaterialIndex, int accentMaterialIndex)
    {
        // destroy all children under player body transform. yea destroying everything every time one part changes is bad optimization but here it hopefully shouldn't matter
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        // instantiate body of character
        Transform characterBody = Instantiate(bodies[bodyIndex], transform).transform;
        characterBody.GetComponent<Renderer>().material = bodyMaterials[bodyMaterialIndex];
        
        // instantiate each body part on the body pivot points
        Renderer[] legRenderer = Instantiate(legs[legIndex], characterBody.Find("Pivot_Legs")).transform.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in legRenderer) // assign materials to instantiated object
            r.material = accentMaterials[accentMaterialIndex];
        
        Renderer bagRenderer = Instantiate(bags[bagIndex], characterBody.Find("Pivot_Bag")).GetComponent<Renderer>();
        if (bagRenderer.transform.childCount > 0)
        {
            foreach (Renderer r in bagRenderer.transform.GetComponentsInChildren<Renderer>())
                r.material = bagMaterials[bagMaterialIndex];
        }else
            bagRenderer.material = bagMaterials[bagMaterialIndex];
        
        Renderer leftEyeRenderer = Instantiate(leftEyes[leftEyeIndex], characterBody.Find("Pivot_EyeL")).GetComponent<Renderer>();
        if (leftEyeRenderer.transform.childCount > 0)
            leftEyeRenderer.material = accentMaterials[accentMaterialIndex];
        else if (leftEyeRenderer.materials.Length > 1)
        {
            Material[] materials = new Material[accentMaterials.Length];
            materials[0] = leftEyeRenderer.materials[0];
            materials[1] = accentMaterials[accentMaterialIndex];
            
            leftEyeRenderer.materials = materials;
        }
        
        Renderer rightEyeRenderer = Instantiate(rightEyes[rightEyeIndex], characterBody.Find("Pivot_EyeR")).GetComponent<Renderer>();
        if (rightEyeRenderer.transform.childCount > 0)
            rightEyeRenderer.material = accentMaterials[accentMaterialIndex];
        else if (rightEyeRenderer.materials.Length > 1)
        {
            Material[] materials = new Material[accentMaterials.Length];
            materials[0] = rightEyeRenderer.materials[0];
            materials[1] = accentMaterials[accentMaterialIndex];
            
            rightEyeRenderer.materials = materials;

        }
        
        Renderer mouthRenderer = Instantiate(mouths[mouthIndex], characterBody.Find("Pivot_Mouth")).GetComponent<Renderer>();
        if (mouthRenderer.materials.Length > 1) // this is only for the tongue mouth right now
        {
            Material[] materials = new Material[accentMaterials.Length];
            materials[0] = mouthRenderer.materials[0];
            materials[1] = accentMaterials[accentMaterialIndex];
            
            mouthRenderer.materials = materials;

        }
        
        // depending on leg index move the character body up so feet are on the ground
        characterBody.position = characterBody.Find("Pivot_Legs").position;
        switch (legIndex) // this shit works bad but I can't be bothered rn
        {
            case 0:
                characterBody.Translate(-characterBody.up * shortLegLength);
                break;
            
            case 1:
                characterBody.Translate(-characterBody.up * normalLegLength);
                break;
            
            case 2:
                characterBody.Translate(-characterBody.up * longLegLength);
                break;
        }
    }
}
