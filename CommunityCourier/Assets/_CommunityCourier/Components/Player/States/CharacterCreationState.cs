using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;

public class CharacterCreationState : StateNode
{
    [SerializeField] private CharacterCreator characterCreatorPrefab;
    private CharacterCreator characterCreatorInstance;

    private LoadStartingScene spawnStateNode;

    public override void Enter()
    {
        base.Enter();

        if (characterCreatorInstance != null || !machine.isOwner)
            return;

        spawnStateNode = machine.transform.GetComponentInChildren<LoadStartingScene>();
        
        InstantiateCharacterCreator();
    }

    private void InstantiateCharacterCreator()
    {
        characterCreatorInstance = Instantiate(characterCreatorPrefab, transform);
        characterCreatorInstance.GiveOwnership(localPlayer);
    }
}
