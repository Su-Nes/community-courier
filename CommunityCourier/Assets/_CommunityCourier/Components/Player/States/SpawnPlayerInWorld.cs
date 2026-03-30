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

        StartCoroutine(SpawnInRandomScene());
    }

    private IEnumerator SpawnInRandomScene()
    {
        SceneLoadManager.instance.LoadRandomIsland();
        
        yield return new WaitForSeconds(sceneLoadBuffer);
print(SceneManager.GetActiveScene().GetRootGameObjects()[0].name);
        player.transform.position = SceneManager.GetActiveScene().GetRootGameObjects()[0].transform.position +
                                    Vector3.up * spawnDistanceFromGround;
        
        player.GiveOwnership(machine.owner);
        player.SetActivity(true);
    }
}
