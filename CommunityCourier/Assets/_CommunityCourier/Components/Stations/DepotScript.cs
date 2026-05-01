using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using UnityEngine.Serialization;

public class DepotScript : NetworkBehaviour
{
    [SerializeField] private DeliveryPackage packagePrefab;
    [SerializeField] private Transform packageHolder;
    [SerializeField] private Transform[] packageSpawnPoints;

    public void SpawnPackage()
    {
        DeliveryPackage newPackage = Instantiate(packagePrefab, packageSpawnPoints[packageHolder.childCount].position, transform.rotation, packageHolder);
        newPackage.InitializePackage(this);
    }

    public bool IsFull()
    {
        return packageHolder.childCount >= packageSpawnPoints.Length;
    }
}
