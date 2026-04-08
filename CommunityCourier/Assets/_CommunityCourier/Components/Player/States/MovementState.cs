using System.Collections;
using System.Collections.Generic;
using PurrNet.StateMachine;
using UnityEngine;

public class MovementState : StateNode
{
    [HideInInspector] public PlayerController player;
    
    public override void Enter()
    {
        base.Enter();
        
        player.GiveOwnership(machine.owner);
        player.SetActivity(true);
    }
}
