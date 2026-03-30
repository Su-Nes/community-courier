using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;

public class CharacterCreation : StateNode
{
    [SerializeField] private CharacterCreator characterCreatorPrefab;
    private CharacterCreator characterCreatorInstance;


    public override void Enter(bool asServer)
    {
        base.Enter(asServer);
        
        characterCreatorInstance = Instantiate(characterCreatorPrefab);
        characterCreatorInstance.GiveOwnership(owner);
    }

    public override void Exit(bool asServer)
    {
        base.Exit(asServer);
        
        //characterCreatorInstance.PlayerObject.
    }

    public void CharacterComplete()
    {
        machine.Next();
    }
}
