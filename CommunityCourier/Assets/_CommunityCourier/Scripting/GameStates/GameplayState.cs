using System.Collections;
using System.Collections.Generic;
using PurrNet.StateMachine;
using UnityEngine;

public class GameplayState : StateNode
{
    public override void Enter()
    {
        base.Enter();
        
        print("gayme is playing");
    }
}
