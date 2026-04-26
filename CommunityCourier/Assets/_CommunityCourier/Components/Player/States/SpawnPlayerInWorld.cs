using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayerInWorld : StateNode
{
    [HideInInspector] public PlayerController playerPrefab;
    [SerializeField] private float spawnDistanceFromGround = 30f, sceneLoadBuffer;
    [SerializeField] private MovementState movementState;
    
    public override void Enter()
    {
        base.Enter();

        if (!isOwner)
            return;
        
        SpawnPlayers();
    }

    private IEnumerator SpawnInRandomScene()
    {
        SceneLoadManager.instance.LoadRandomIsland();

        yield return new WaitForSeconds(sceneLoadBuffer);
        Vector3 spawnPos = FindFirstObjectByType<IslandArrivalPoint>().transform.position +
                           Vector3.up * spawnDistanceFromGround;
        
        for (int i = 0; i < 60; i++) // it's so stupid that I have to do this. maybe it's a problem with the network transform?
        {
            playerPrefab.transform.position = spawnPos;
            yield return null;
        }

        movementState.player = playerPrefab;
        machine.SetState(movementState);
    }
    
    private void SpawnPlayers()
    {
        foreach (var player in networkManager.players)
        {
            var playerInstance = Instantiate(playerPrefab);
            playerInstance.GiveOwnership(player);
            playerInstance.gameObject.name = $"Player {player.id}";
        }

        StartCoroutine(SpawnInRandomScene());
    }
}
