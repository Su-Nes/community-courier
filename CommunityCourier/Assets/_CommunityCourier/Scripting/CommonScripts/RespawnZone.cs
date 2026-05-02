using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private Transform respawnPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
            StartCoroutine(StupidNetworkSetPosition(other.transform));
    }

    private IEnumerator StupidNetworkSetPosition(Transform player)
    {
        for (int i = 0; i < 60; i++) // it's so stupid that I have to do this. maybe it's a problem with the network transform?
        {
            player.position = respawnPoint.position;
            yield return null;
        }
    }
}
