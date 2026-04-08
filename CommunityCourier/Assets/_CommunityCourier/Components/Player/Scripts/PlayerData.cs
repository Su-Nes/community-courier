using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public class PlayerData
{
    private PlayerID playerID;
    
    private int bodyIndex,
        legIndex,
        bagIndex,
        leftEyeIndex,
        rightEyeIndex,
        mouthIndex,
        bodyMaterialIndex,
        bagMaterialIndex,
        accentMaterialIndex;

    public void SetPlayerID(PlayerID ID)
    {
        playerID = ID;
    }
    
    public void SetCharacterBuildData(int body, int leg, int bag, int leftEye, int rightEye, int mouth, int bodyMaterial, int bagMaterial, int accentMaterial)
    {
        bodyIndex = body;
        legIndex = leg;
        bagIndex = bag;
        leftEyeIndex = leftEye;
        rightEyeIndex = rightEye;
        mouthIndex = mouth;
        bodyMaterialIndex = bodyMaterial;
        bagMaterialIndex = bagMaterial;
        accentMaterialIndex = accentMaterial;
    }
}
