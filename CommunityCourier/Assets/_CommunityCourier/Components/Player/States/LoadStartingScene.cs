using System.Collections;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStartingScene : StateNode
{
    [HideInInspector] public PlayerController playerPrefab;
    [SerializeField] private float spawnDistanceFromGround = 30f;
    private MovementState movementState;
    
    public override void Enter()
    {
        base.Enter();

        if (!machine.isOwner)
            return;
        
        movementState = machine.transform.GetComponentInChildren<MovementState>();

        StartCoroutine(SpawnInRandomScene());
    }

    private IEnumerator SpawnInRandomScene()
    {
        PlayerCanvasManager.instance.SetLoadingScreen(true);
        SceneLoadManager.instance.LoadRandomIsland();

        while (SceneLoadManager.instance.IsLoading)
            yield return null;
        
        Vector3 spawnPos = FindFirstObjectByType<IslandArrivalPoint>().transform.position +
                           Vector3.up * spawnDistanceFromGround;

        PlayerCanvasManager.instance.SetLoadingScreen(false);
        
        movementState.startingPosition = spawnPos;
        movementState.player = playerPrefab;
        machine.SetState(movementState);
    }
}
