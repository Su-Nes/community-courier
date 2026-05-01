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
    [SerializeField] private Transform giantArrow;
    private Transform arrowInstance;
    [SerializeField] private Vector3 arrowPosOffset;

    private float revenue, actualProfit;
    
    private DeliveryPackage heldPackage;
    public DeliveryPackage Package => heldPackage;

    private void Start()
    {
        packageUI.SetActive(false);
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

    public void TakePackage(DeliveryPackage package)
    {
        if (heldPackage != null)
            DropPackage(heldPackage);

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

        SetArrowPosition(package);
    }

    private void SetArrowPosition(DeliveryPackage package)
    {
        arrowInstance = Instantiate(giantArrow);
        arrowInstance.position = package.TargetDepot.value.transform.position + arrowPosOffset;
    }

    private void DisableArrow()
    {
        if (arrowInstance != null)
            Destroy(arrowInstance.gameObject);
    }

    public void DropPackage(DeliveryPackage package)
    {
        package.SetPackageActive(true);
        //package.transform.SetParent(null);
        package.SetPosition(transform.position);
        
        heldPackage = null;
        
        packageUI.SetActive(false);
        DisableArrow();
    }
}
