using System.Collections;
using PurrNet;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryPackage : InteractableScript
{
    [SerializeField] private float costMin = 8.12f, costMax = 25.3f;
    public SyncVar<float> Cost = new();
    [SerializeField] private string[] randAdjectives, randNames;
    public SyncVar<DepotScript> OriginDepot = new();
    public SyncVar<DepotScript> TargetDepot = new();
    
    
    public void InitializePackage(DepotScript startDepot)
    {
        StartCoroutine(WaitAndInitialize(startDepot));
    }

    [ObserversRpc]
    public void SetPackageActive(bool activity)
    {
        gameObject.SetActive(activity);
    }

    private IEnumerator WaitAndInitialize(DepotScript startDepot) // spaghetti cause I can't be asked to bring this back to how it was earlier before bug fixing attempts
    {
        DeliveryManager deliveries = FindObjectOfType<DeliveryManager>();
        
        string randName = $"{randAdjectives[Random.Range(0, randAdjectives.Length)]} {randNames[Random.Range(0, randNames.Length)]}";
        
        DepotScript randTargetDepot = new DepotScript();
        DepotScript[] depots = FindObjectsOfType<DepotScript>();
        while (true) // assign random depot that isn't the origin depot
        {
            randTargetDepot = depots[Random.Range(0, depots.Length)];

            if (randTargetDepot != OriginDepot.value)
                break;
        }
        
        float randCost = Random.Range(costMin, costMax);
        randCost = Mathf.Round(randCost * 100f) / 100.0f;
        
        SetParamsForAll(randName, startDepot, randTargetDepot, randCost);
        
        deliveries.AddPackage(this);
        
        yield return null;
    }

    [ObserversRpc]
    public void SetParamsForAll(string packageName, DepotScript startDepot, DepotScript target, float cost)
    {
        gameObject.name = packageName;
        OriginDepot.value = startDepot;
        TargetDepot.value = target;
        Cost.value = cost;
    }

    [ObserversRpc]
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
    
    public override void Interact(InteractionManager interactor)
    {
        interactor.packageManager.TakePackage(this);
    }
}
