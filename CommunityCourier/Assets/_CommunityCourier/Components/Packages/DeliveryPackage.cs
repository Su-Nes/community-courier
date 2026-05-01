using PurrNet;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryPackage : InteractableScript
{
    [SerializeField] private float costMin = 8.12f, costMax = 25.3f;
    public SyncVar<float> Cost = new(ownerAuth: true);
    [SerializeField] private string[] randAdjectives, randNames;
    public SyncVar<DepotScript> OriginDepot = new(ownerAuth: true);
    public SyncVar<DepotScript> TargetDepot = new(ownerAuth: true);
    

    public void InitializePackage(DepotScript startDepot)
    {
        DeliveryManager deliveries = FindObjectOfType<DeliveryManager>();

        SetName($"{randAdjectives[Random.Range(0, randAdjectives.Length)]} {randNames[Random.Range(0, randNames.Length)]}");
        
        OriginDepot.value = startDepot;

        while (true) // assign random depot that isn't the origin depot
        {
            TargetDepot.value = deliveries.Depots[Random.Range(0, deliveries.Depots.Count)];

            if (TargetDepot != OriginDepot)
                break;
        }
        
        Cost.value = Random.Range(costMin, costMax);
        Cost.value = Mathf.Round(Cost * 100f) / 100.0f;
        
        deliveries.AddPackage(this);
    }

    [ObserversRpc]
    public void SetPackageActive(bool activity)
    {
        gameObject.SetActive(activity);
    }

    [ObserversRpc]
    public void SetName(string newName)
    {
        gameObject.name = newName;
    }

    public override void Interact(InteractionManager interactor)
    {
        interactor.packageManager.TakePackage(this);
    }
}
