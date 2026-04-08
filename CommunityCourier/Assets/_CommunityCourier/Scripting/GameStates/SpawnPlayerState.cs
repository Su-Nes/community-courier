using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;

public class SpawnPlayerState : StateNode
{
    [SerializeField] private StateMachine playerStateMachine;
    private StateMachine playerInstance;

    public override void Enter(bool asServer)
    {
        base.Enter();

        if (!asServer)
            return;
        
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        int playerIndex = 0;
        foreach (var player in networkManager.players)
        {
            playerInstance = Instantiate(playerStateMachine, Vector3.right * playerIndex * 100f + Vector3.up * 100f, Quaternion.identity);
            playerInstance.GiveOwnership(player);
            playerInstance.gameObject.name = $"Player {player.id}";
            playerIndex++;
        }

        machine.Next();
    }

    public override void Exit()
    {
        
    }
}
