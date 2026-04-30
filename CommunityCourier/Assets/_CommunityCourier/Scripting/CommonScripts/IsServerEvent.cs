using System.Collections;
using System.Collections.Generic;
using PurrNet;
using UnityEngine;
using UnityEngine.Events;

public class IsServerEvent : NetworkBehaviour
{
    [SerializeField] private UnityEvent onServer, onNotServer;
    
    private void Start()
    {
        if (networkManager.isServer)
            onServer.Invoke();
        else 
            onNotServer.Invoke();
    }
}
