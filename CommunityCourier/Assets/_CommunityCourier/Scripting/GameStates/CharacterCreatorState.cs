using System.Collections;
using System.Collections.Generic;
using PurrNet.StateMachine;
using UnityEngine;

public class CharacterCreatorState : StateNode
{
    public override void Enter(bool asServer)
    {
        base.Enter(asServer);
    }

    public void CharacterComplete()
    {
        machine.Next();
    }
}
