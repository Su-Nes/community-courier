using System;
using System.Collections;
using System.Collections.Generic;
using PurrNet;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GlobalMoney : NetworkBehaviour
{
    public static GlobalMoney instance;
    
    [SerializeField] private float moneyGoal = 100f;
    private float currentMoney;
    [SerializeField] private TMP_Text[] goalText;
    [SerializeField] private TMP_Text winText;
    [SerializeField] private string[] winQuotes;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
        
        AssignText();
    }

    [ObserversRpc]
    public void AddMoney(float amount)
    {
        currentMoney += amount;
        AssignText();

        if (currentMoney >= moneyGoal)
            StartCoroutine(Win());
    }
    
    private IEnumerator Win()
    {
        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            player.GetComponent<Collider>().enabled = false;
            Destroy(player.GetComponent<CharacterController>());
            player.SetActivity(false);
        }
            
        
        int limiter = 0;
        while (limiter < 999)
        {
            foreach (PlayerController player in FindObjectsOfType<PlayerController>()) // this is meant to make your game lag and crash on purpose
            {
                TMP_Text newText = Instantiate(winText, player.transform.position, Quaternion.identity);
                newText.text = winQuotes[Random.Range(0, winQuotes.Length)];
                newText.GetComponent<Rigidbody>().isKinematic = false;
                newText.transform.localScale *= Random.Range(3f, 15f);
            }
            
            yield return new WaitForSeconds(.5f);
            
            limiter++;
        }
    }

    private void AssignText()
    {
        foreach (TMP_Text goal in goalText)
        {
            goal.text = $"GOAL: {currentMoney}$/{moneyGoal}.00$";
        }
    }
}
