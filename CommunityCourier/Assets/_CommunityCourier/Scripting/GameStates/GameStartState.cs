using System.Collections;
using System.Collections.Generic;
using PurrNet.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class GameStartState : StateNode
{
    [SerializeField] private GameObject startUIAdmin, startUIClient;
    private GameObject uiInstance;

    public override void Enter(bool asServer)
    {
        base.Enter(asServer);

        if (uiInstance != null)
            return;
            
        if (asServer)
        {
            uiInstance = Instantiate(startUIAdmin);
            if (uiInstance.transform.Find("Button_Start") != null)
                uiInstance.transform.Find("Button_Start").GetComponent<Button>().onClick.AddListener(StartGame);
            else 
                Debug.LogError("Missing 'Button_Start' on Start UI prefab!");
        }
        else
            uiInstance = Instantiate(startUIClient);
            
    }

    public override void Exit(bool asServer)
    {
        base.Exit(asServer);
        
        if (!asServer)
            Destroy(uiInstance);
    }

    public void StartGame()
    {
        Destroy(uiInstance);
        machine.Next();
    }
}
