using System;
using PurrNet;
using TMPro;
using UnityEngine;

public class PlayerCounter : NetworkBehaviour
{
    private void OnEnable()
    {
        networkManager.onPlayerJoined += OnPlayerJoined;
        networkManager.onPlayerLeft += OnPlayerLeft;
    }

    private void OnDisable()
    {
        networkManager.onPlayerJoined -= OnPlayerJoined;
        networkManager.onPlayerLeft -= OnPlayerLeft;
    }

    private void OnPlayerJoined(PlayerID player, bool smth, bool smth2)
    {
        ChangePlayerCountText();
    }
    
    private void OnPlayerLeft(PlayerID player, bool smth)
    {
        ChangePlayerCountText();
    }

    private void ChangePlayerCountText()
    {
        GetComponent<TMP_Text>().text = $"Player count: {networkManager.playerCount}/100";
    }
}
