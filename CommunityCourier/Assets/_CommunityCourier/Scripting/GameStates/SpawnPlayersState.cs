using System.Collections;
using System.Collections.Generic;
using PurrNet.StateMachine;
using UnityEngine;

public class SpawnPlayersState : StateNode
{
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private List<Transform> spawnPoints = new();

    public override void Enter(bool asServer)
    {
        base.Enter();

        if (!asServer)
            return;

        SpawnPlayers();

        //machine.Next();
    }

    private void SpawnPlayers()
    {
        int currentSpawnIndex = 0;
        foreach (var player in networkManager.players)
        {
            var spawnPoint = spawnPoints[currentSpawnIndex];
            var newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            newPlayer.GiveOwnership(player);
            currentSpawnIndex++;
            
            if (currentSpawnIndex >= spawnPoints.Count)
                currentSpawnIndex = 0;
        }
    }

    public override void Exit()
    {
        
    }
}
