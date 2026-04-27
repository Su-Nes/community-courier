using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryPackage : MonoBehaviour
{
    [SerializeField] private float costMin = 8.12f, costMax = 25.3f;
    public float Cost { get; private set; }
    [SerializeField] private string[] randAdjectives, randNames;
    private DepotScript originDepot, targetDepot;
    public DepotScript OriginDepot => originDepot;
    public DepotScript TargetDepot => targetDepot;
    

    public void InitializePackage(DepotScript startDepot)
    {
        DeliveryManager deliveries = FindObjectOfType<DeliveryManager>();
        
        gameObject.name = $"{randAdjectives[Random.Range(0, randAdjectives.Length)]} {randNames[Random.Range(0, randNames.Length)]}";
        
        originDepot = startDepot;

        while (true) // assign random depot that isn't the origin depot
        {
            targetDepot = deliveries.Depots[Random.Range(0, deliveries.Depots.Count)];

            if (targetDepot != originDepot)
                break;
        }
        
        Cost = Random.Range(costMin, costMax);
        Cost = Mathf.Round(Cost * 100f) / 100.0f;
        
        print($"{gameObject.name} {Cost}$. deliver to {targetDepot.gameObject.name}");
        
        deliveries.AddPackage(this);
    }
}
