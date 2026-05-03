using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleEventWithKey : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private bool toggle;
    [SerializeField] private UnityEvent on, off;
    
    
    private void Start()
    {
        if (toggle)
            on.Invoke();
        else
            off.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
            Toggle();
    }

    private void Toggle()
    {
        toggle = !toggle;
        
        if (toggle)
            on.Invoke();
        else
            off.Invoke();
    }
}
