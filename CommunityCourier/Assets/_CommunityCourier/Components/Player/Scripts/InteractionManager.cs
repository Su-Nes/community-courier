using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRadius;
    public PackageManager packageManager;

    private void Update()
    {
        if (InputManager.instance.inputActionAsset.FindAction("Interact").IsPressed())
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Collider[] interactables = Physics.OverlapSphere(interactionPoint.position, interactionRadius, layerMask);

        if (interactables.Length == 0)
            return;
        
        interactables[0].TryGetComponent(out InteractableScript interactable);
        interactable.Interact(this);
    }
}
