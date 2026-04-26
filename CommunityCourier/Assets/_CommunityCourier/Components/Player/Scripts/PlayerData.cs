using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public class PlayerData
{
    public PlayerID ID;
    
    public int BodyIndex,
        LegIndex,
        BagIndex,
        LeftEyeIndex,
        RightEyeIndex,
        MouthIndex,
        BodyMaterialIndex,
        BagMaterialIndex,
        AccentMaterialIndex;

    public void SetPlayerID(PlayerID id)
    {
        this.ID = id;
    }
    
    public void SetCharacterBuildData(int body, int leg, int bag, int leftEye, int rightEye, int mouth, int bodyMaterial, int bagMaterial, int accentMaterial)
    {
        BodyIndex = body;
        LegIndex = leg;
        BagIndex = bag;
        LeftEyeIndex = leftEye;
        RightEyeIndex = rightEye;
        MouthIndex = mouth;
        BodyMaterialIndex = bodyMaterial;
        BagMaterialIndex = bagMaterial;
        AccentMaterialIndex = accentMaterial;
    }
}
