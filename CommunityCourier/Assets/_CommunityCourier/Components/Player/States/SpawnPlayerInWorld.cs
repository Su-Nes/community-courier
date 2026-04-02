using System.Collections;
using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayerInWorld : StateNode
{
    public PlayerController player;
    [SerializeField] private float spawnDistanceFromGround = 30f, sceneLoadBuffer;
    
    public override void Enter()
    {
        base.Enter();

        if(!isOwner)
            return;
        
        StartCoroutine(SpawnInRandomScene());
    }

    private IEnumerator SpawnInRandomScene()
    {
        SceneLoadManager.instance.LoadRandomIsland();
        player.GiveOwnership(machine.owner);

        yield return new WaitForSeconds(sceneLoadBuffer);
        Vector3 spawnPos = FindFirstObjectByType<IslandArrivalPoint>().transform.position +
                           Vector3.up * spawnDistanceFromGround;
        
        for (int i = 0; i < 60; i++) // it's so stupid that I have to do this. maybe it's a problem with the network transform?
        {
            player.transform.position = spawnPos;
            yield return null;
        }
        
        player.SetActivity(true);
    }
}
