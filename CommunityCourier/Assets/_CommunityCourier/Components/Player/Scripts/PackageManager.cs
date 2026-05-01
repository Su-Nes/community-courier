using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PackageManager : MonoBehaviour
{
    [SerializeField] private Canvas packageCanvas;
    [SerializeField] private TMP_Text packageName, packageDestination, packageCost;
    
    private DeliveryPackage heldPackage;

    private void Start()
    {
        packageCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.instance.inputActionAsset.FindAction("Drop").IsPressed() && heldPackage != null)
            DropPackage(heldPackage);
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
        packageCanvas.gameObject.SetActive(true);
        
        packageName.text = $"Current delivery:\n{package.gameObject.name}";
        packageDestination.text = $"Destination: {package.TargetDepot.value.gameObject.name}";
        packageCost.text = $"Cost: {package.Cost}$";
    }

    public void DropPackage(DeliveryPackage package)
    {
        package.SetPackageActive(true);
        //package.transform.SetParent(null);
        package.SetPosition(transform.position);
        
        heldPackage = null;
        
        packageCanvas.gameObject.SetActive(false);
    }
}
