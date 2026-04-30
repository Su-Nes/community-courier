using System.Collections;
using System.Collections.Generic;
using PurrNet.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class GameStartState : StateNode
{
    [SerializeField] private GameObject startUI;
    private GameObject uiInstance;

    public override void Enter()
    {
        base.Enter();
        
        uiInstance = Instantiate(startUI);
        if (uiInstance.transform.Find("Button_Start") != null)
            uiInstance.transform.Find("Button_Start").GetComponent<Button>().onClick.AddListener(StartGame);
    }

    public override void Exit(bool asServer)
    {
        base.Exit(asServer);
        
        Destroy(uiInstance);
    }

    public void StartGame()
    {
        machine.Next();
    }
}
