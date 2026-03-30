using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;

public class SpawnPlayersState : StateNode
{
    [SerializeField] private StateMachine playerStateMachine;

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
            var spawnPoint = Vector3.forward * 100f * currentSpawnIndex; // space out the players
            var newPlayer = Instantiate(playerStateMachine, spawnPoint, Quaternion.identity);
            newPlayer.GiveOwnership(player);
            currentSpawnIndex++;
        }
    }

    public override void Exit()
    {
        
    }
}
