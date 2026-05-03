using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PackageManager : MonoBehaviour
{
    [SerializeField] private GameObject packageUI;
    [SerializeField] private TMP_Text packageName, packageDestination, packageCost, revenueText;
    [SerializeField] private float dropDistance = 1.5f;

    private float revenue;
    public float Revenue => revenue;
    
    private DeliveryPackage heldPackage;
    public DeliveryPackage Package => heldPackage;
    
    private PlayerController playerController;

    private void Start()
    {
        packageUI.SetActive(false);
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (InputManager.instance.inputActionAsset.FindAction("Drop").IsPressed() && heldPackage != null)
            DropPackage(heldPackage);
    }

    public void AddGrossProfit(float profit)
    {
        revenue += profit;
        revenueText.text = $"Revenue: {revenue}$";
    }

    public void RemoveRevenue()
    {
        revenue = 0f;
        revenueText.text = $"Revenue: {revenue}$";
    }

    public void TakePackage(DeliveryPackage package)
    {
        if (heldPackage != null)
            return;

        AssignPackage(package);
    }

    private void AssignPackage(DeliveryPackage package)
    {
        heldPackage = package;
        //package.transform.SetParent(transform);
        package.SetPackageActive(false);
        
        AssignUI(package);
    }

    private void AssignUI(DeliveryPackage package)
    {
        packageUI.SetActive(true);
        
        packageName.text = $"Current delivery:\n{package.gameObject.name}";
        packageDestination.text = $"Destination: {package.TargetDepot.value.gameObject.name}";
        packageCost.text = $"Cost: {package.Cost}$";
    }

    public void DropPackage(DeliveryPackage package)
    {
        package.SetPackageActive(true);
        //package.transform.SetParent(null);
        package.SetPosition(transform.position - playerController.BodyTransform.forward * dropDistance + Vector3.up * dropDistance);
        
        heldPackage = null;
        
        packageUI.SetActive(false);
    }
}
