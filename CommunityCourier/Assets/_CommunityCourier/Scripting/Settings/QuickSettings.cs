using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSettings : MonoBehaviour
{
    private bool isFullscreen;

    private void Start()
    {
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.ExclusiveFullScreen:
                isFullscreen = true;
                break;
            
            case FullScreenMode.FullScreenWindow:
                isFullscreen = true;
                break;
            
            case FullScreenMode.Windowed:
                isFullscreen = false;
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFullscreen();
        }
    }

    private void ToggleFullscreen()
    {
        Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
}
