using System;
using PurrNet;
using TMPro;
using UnityEngine;

public class PlayerCounter : NetworkBehaviour
{

    private void Update()
    {
        if (networkManager == null)
            return;
        
        ChangePlayerCountText();
    }

    private void ChangePlayerCountText()
    {
        GetComponent<TMP_Text>().text = $"Player count: {networkManager.playerCount}/100";
    }
}
