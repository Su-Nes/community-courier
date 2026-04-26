using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasManager : MonoBehaviour
{
    public static PlayerCanvasManager instance;

    [SerializeField] private GameObject loadingScreen;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        loadingScreen.SetActive(false);
    }

    public void SetLoadingScreen(bool state)
    {
        loadingScreen.SetActive(state);
    }
}
