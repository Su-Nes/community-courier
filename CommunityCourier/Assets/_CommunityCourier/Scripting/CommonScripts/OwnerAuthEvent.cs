using PurrNet;
using PurrNet.StateMachine;
using UnityEngine;
using UnityEngine.Events;

public class OwnerAuthEvent : NetworkBehaviour
{
    [SerializeField] private NetworkBehaviour networkObject;
    [SerializeField] private UnityEvent onOwner, onNotOwner;
    
    private void Start()
    {
        if (networkObject.owner == networkManager.localPlayer)
            onOwner.Invoke();
        else 
            onNotOwner.Invoke();
    }
}
