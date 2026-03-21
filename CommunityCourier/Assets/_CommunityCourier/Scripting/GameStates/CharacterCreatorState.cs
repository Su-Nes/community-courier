using System.Collections;
using System.Collections.Generic;
using PurrNet.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreatorState : StateNode
{
    [SerializeField] private CharacterCreator characterCreatorPrefab;
    
    public override void Enter(bool asServer)
    {
        base.Enter(asServer);

        SpawnCharacterCreation();
    }
    
    private void SpawnCharacterCreation()
    {
        int currentSpawnIndex = 0;
        foreach (var player in networkManager.players)
        {
            var spawnPoint = Vector3.forward * 100f * currentSpawnIndex;
            var newPlayer = Instantiate(characterCreatorPrefab, spawnPoint, Quaternion.identity);
            newPlayer.GiveOwnership(player);
            currentSpawnIndex++;
        }
    }

    public void CharacterComplete()
    {
        machine.Next();
    }
}
