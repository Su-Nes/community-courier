using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryManager : NetworkBehaviour
{
    
    
    public static DeliveryManager instance;
    
    private List<DepotScript> depots;

    [SerializeField] private float restockRate = 30f;
    private float t;
    [SerializeField] private int restockBatch = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += AssignDepots;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= AssignDepots;
    }

    private void AssignDepots(Scene scene, Scene otherScene)
    {
        foreach(var depot in FindObjectsOfType<DepotScript>())
            depots.Add(depot);
    }

    private void Update()
    {
        if (depots.Count <= 0)
            return;

        if (t <= 0f)
        {
            for (int i = 0; i < restockBatch; i++)
                StockDepots();
        }
    }

    private void StockDepots()
    {
        
    }
}
