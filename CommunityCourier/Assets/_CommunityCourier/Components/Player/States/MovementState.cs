using System.Collections;
using System.Collections.Generic;
using PurrNet.StateMachine;
using UnityEngine;

public class MovementState : StateNode
{
    [HideInInspector] public PlayerController player;
    [HideInInspector] public Vector3 startingPosition;
    
    public override void Enter()
    {
        base.Enter();

        if (!machine.isOwner)
            return;
        
        player.GiveOwnership(machine.owner);
        player.SetActivity(true);
        
        player.transform.position = startingPosition;
    }
}
