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
        StartCoroutine(SpawnPlayers());
    }

    [ServerRpc(requireOwnership: false)]
    public void AddPlayer(PlayerData player)
    {
        if (ConnectedPlayers.Contains(player))
            return; 
        
        ConnectedPlayers.Add(player);

        foreach (PlayerData playerData in ConnectedPlayers)
        {
            print(playerData.ID.ToString());
        }
    }

    private IEnumerator SpawnPlayers()
    {
        yield return new WaitForSeconds(1f);
        print(networkManager.playerCount);
        foreach (var player in networkManager.players)
        {
            StateMachine newPlayer = Instantiate(playerObject);
            newPlayer.GiveOwnership(player);
        }
    }
}
