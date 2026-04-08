using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public class ConnectedPlayerManager : NetworkBehaviour
{
    private SyncArray<PlayerData> ConnectedPlayers;
    
    public static ConnectedPlayerManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPlayer(PlayerData player)
    {
        ConnectedPlayers.Add(player);
    }
}
