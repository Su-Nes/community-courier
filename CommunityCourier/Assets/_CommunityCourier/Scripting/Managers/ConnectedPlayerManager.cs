using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;

public class ConnectedPlayerManager : NetworkBehaviour
{
    [SerializeField] private StateMachine playerObject;
    private SyncList<PlayerData> ConnectedPlayers = new(ownerAuth: false);
    
    public static ConnectedPlayerManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        AddPlayer(new PlayerData());
    }

    [ServerRpc(requireOwnership: false)]
    public void AddPlayer(PlayerData player)
    {
        if (ConnectedPlayers.Contains(player))
            return; 
        
        ConnectedPlayers.Add(player);
    }

    private IEnumerator SpawnPlayers()
    {
        yield return new WaitForSeconds(1f);
        foreach (var player in networkManager.players)
        {
            StateMachine newPlayer = Instantiate(playerObject);
            newPlayer.GiveOwnership(player);
        }
    }
}
