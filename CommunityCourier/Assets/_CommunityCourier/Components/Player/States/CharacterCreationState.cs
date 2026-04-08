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

    public override void Enter()
    {
        base.Enter();

        if (!machine.isOwner)
            return;

        if (characterCreatorInstance != null)
            return;
        
        characterCreatorInstance = Instantiate(characterCreatorPrefab, transform);
        characterCreatorInstance.GiveOwnership(machine.owner);
    }

    public void CharacterComplete()
    {
        characterCreatorInstance.PlayerObject.PlayerCamera.gameObject.SetActive(true);
        characterCreatorInstance.PlayerObject.transform.SetParent(transform.parent); // yoink the player object out of the character creation prefab 
        characterCreatorInstance.PlayerObject.BodyTransform.GetComponent<CharacterAnimator>().GiveOwnership(machine.owner);
        
        spawnStateNode.player = characterCreatorInstance.PlayerObject;
        characterCreatorInstance.gameObject.SetActive(false);
        machine.SetState(spawnStateNode);
    }
}
