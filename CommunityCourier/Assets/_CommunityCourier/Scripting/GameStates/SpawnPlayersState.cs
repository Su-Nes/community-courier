using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;

public class SpawnPlayersState : StateNode
{
    [SerializeField] private StateMachine playerStateMachine;
    private StateMachine playerInstance;

    public override void Enter(bool asServer)
    {
        base.Enter();

        if (playerInstance != null || !asServer)
            return;
        
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        playerInstance = Instantiate(playerStateMachine, transform);
        playerInstance.GiveOwnership(networkManager.localPlayer);
    }

    public override void Exit()
    {
        
    }
}
