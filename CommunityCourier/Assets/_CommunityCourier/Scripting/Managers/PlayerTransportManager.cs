using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransportManager : MonoBehaviour
{
    public static PlayerTransportManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }
}
