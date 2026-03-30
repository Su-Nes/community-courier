using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;

public class CharacterCreationState : StateNode
{
    [SerializeField] private CharacterCreator characterCreatorPrefab;
    private CharacterCreator characterCreatorInstance;

    [SerializeField] private SpawnPlayerInWorld spawnStateNode;

    public override void Enter(bool asServer)
    {
        base.Enter(asServer);

        if (characterCreatorInstance != null)
            return;
        characterCreatorInstance = Instantiate(characterCreatorPrefab, transform);
        characterCreatorInstance.GiveOwnership(owner);
    }

    public override void Exit(bool asServer)
    {
        base.Exit(asServer);
        
        //characterCreatorInstance.PlayerObject.
    }

    public void CharacterComplete()
    {
        characterCreatorInstance.PlayerObject.PlayerCamera.gameObject.SetActive(true);
        characterCreatorInstance.PlayerObject.transform.SetParent(transform.parent); // yoink the player object out of the character creation prefab 
        
        spawnStateNode.player = characterCreatorInstance.PlayerObject;
        Destroy(characterCreatorInstance.gameObject);
        machine.SetState(spawnStateNode);
    }
}
