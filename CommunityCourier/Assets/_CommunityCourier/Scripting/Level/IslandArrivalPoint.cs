using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandArrivalPoint : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    public Transform GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
