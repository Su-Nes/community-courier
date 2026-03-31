using System.Collections;
using System.Collections.Generic;
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
        player.SetActivity(true);
        
        yield return new WaitForSeconds(sceneLoadBuffer);
        Vector3 spawnPos = FindFirstObjectByType<IslandArrivalPoint>().transform.position +
                           Vector3.up * spawnDistanceFromGround;
        
        print(spawnPos);
        player.transform.position.Set(spawnPos.x, spawnPos.y, spawnPos.z);
    }
}
