using System.Collections.Generic;
using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryManager : StateNode
{
    private SyncList<DeliveryPackage> packages = new(ownerAuth: false);
    
    private List<DepotScript> depots = new();
    public List<DepotScript> Depots => depots;

    [SerializeField] private float restockInterval = 30f;
    private float t;
    [SerializeField] private int restockBatch = 3;

    public override void Enter(bool asServer)
    {
        base.Enter(asServer);
        
        enabled = asServer;
        
        if (depots.Count > 0)
            return;
        
        AssignDepots();
    }

    private void AssignDepots()
    {
        foreach(var depot in FindObjectsOfType<DepotScript>())
            depots.Add(depot);
    }

    private void Update()
    {
        if (depots.Count <= 0)
            return;

        t -= Time.deltaTime;
        if (t <= 0f)
        {
            for (int i = 0; i < restockBatch; i++)
                StockDepots();
            t = restockInterval;
        }
    }

    private void StockDepots()
    {
        int maxAttempts = 0;
        while (maxAttempts < 100) // roll until find depot with space
        {
            int randDepot = Random.Range(0, depots.Count);

            if (!depots[randDepot].IsFull())
            {
                depots[randDepot].SpawnPackage();
                break;
            }
            maxAttempts++;
        }
    }

    public void AddPackage(DeliveryPackage package)
    {
        if (packages.Contains(package))
            return;
        
        packages.Add(package);
    }

    public void RemovePackage(DeliveryPackage package)
    {
        if (!packages.Contains(package))
            return;
        
        packages.Remove(package);
    }
}
