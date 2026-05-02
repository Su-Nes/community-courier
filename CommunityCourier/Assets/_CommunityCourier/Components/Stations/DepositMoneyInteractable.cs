using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositMoneyInteractable : InteractableScript
{
    public override void Interact(InteractionManager interactor)
    {
        GlobalMoney.instance.AddMoney(interactor.packageManager.Revenue);
        interactor.packageManager.RemoveRevenue();
    }
}
