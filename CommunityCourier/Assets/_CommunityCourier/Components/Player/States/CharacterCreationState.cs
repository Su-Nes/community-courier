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
        characterCreatorInstance.PlayerObject.GiveOwnership(localPlayer);
    }

    public void CharacterComplete()
    {
        characterCreatorInstance.PlayerObject.PlayerCamera.gameObject.SetActive(true);
        characterCreatorInstance.PlayerObject.transform.SetParent(machine.transform);
        spawnStateNode.playerPrefab = characterCreatorInstance.PlayerObject;
        
        machine.SetState(spawnStateNode);
        characterCreatorInstance.gameObject.SetActive(false);
    }
}
