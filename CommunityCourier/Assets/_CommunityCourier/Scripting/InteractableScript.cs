using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;

public abstract class InteractableScript : NetworkBehaviour
{
    public abstract void Interact(InteractionManager interactor);
    
    // public virtual void OnHover()
}
