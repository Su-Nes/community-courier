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
    }

    private void SpawnPlayers()
    {
        foreach (var player in networkManager.players)
        {
            if (player != localPlayer)
            {
                var playerInstance = Instantiate(playerStateMachine);
                playerInstance.GiveOwnership(player);
                playerInstance.gameObject.name = $"Player {player.id}";
            }
        }
    }
}
