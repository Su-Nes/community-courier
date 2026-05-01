using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDeliveryInteractable : InteractableScript
{
    [SerializeField] private DepotScript thisDepot;

    public override void Interact(InteractionManager interactor)
    {
        if (thisDepot != interactor.packageManager.Package.TargetDepot.value)
            return;
        Debug.LogError("Ignore this error message ^ Everything is working");
        
        interactor.packageManager.Package.DeliverPackage(interactor.packageManager);
    }
}
