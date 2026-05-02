using System.Collections;
using System.Collections.Generic;
using PurrNet;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class StupidChat : NetworkBehaviour
{
    [SerializeField] private PackageManager packageManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform playerBody;
    [SerializeField] private TMP_Text textPrefab, tutorialText;
    [SerializeField] private float textSpacing = .2f;
    [SerializeField] private Vector3 textSpawnOffset;
    public Vector3 TextSpawnOffset => textSpawnOffset;

    [SerializeField] private float characterComboTime = 1f, characterLifetime = 3f;
    private float comboTimer, timeAtLastButtonPress;
    private int characterCombo;

    private List<string> characters = new();
    private bool chatEnabled;
    
    private void Update()
    {
        if (InputManager.instance.inputActionAsset.FindAction("Submit").WasCompletedThisFrame())
            ToggleChatMode();

        if (!chatEnabled)
            return;
        
        //MonitorKeyboard();
    }
    
    private void ToggleChatMode()
    {
        chatEnabled = !chatEnabled;

        tutorialText.text = chatEnabled ? "Enter: " : "Enter: Chat";

        playerController.SetActivity(!chatEnabled);
        packageManager.enabled = !chatEnabled;

        if (!chatEnabled)
            DisplayCharacter(characters, this);
    }

    private void OnGUI()
    {
        if (!chatEnabled || !isOwner)
            return;

        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyUp)
        {
            print(e.type);
            //print($"{(int)e.keyCode}");
            
            string characterToType = e.keyCode.ToString().ToLower();

            switch (characterToType)
            {
                case "space":
                    characterToType = " ";
                    break;
                
                case "return":
                    characterToType = "";
                    break;
                
                case "quote":
                    characterToType = "'";
                    break;
                
                case "backspace":
                    characters.RemoveAt(characters.Count - 1);
                    WriteUI();
                    return;
                
                case "leftshift":
                case "leftcontrol":
                case "rightshift":
                case "rightcontrol":
                case "tab":
                    return;
            }
            
            characters.Add(characterToType);
            WriteUI();
        }
    }

    private void WriteUI()
    {
        string currentWrittenText = "Enter: ";
        foreach (string character in characters)
            currentWrittenText += character;
        tutorialText.text = currentWrittenText;
    }

    [ObserversRpc]
    private void DisplayCharacter(List<string> characterList, StupidChat sender)
    {
        foreach (string character in characterList)
        {
            Vector3 charPosition = sender.transform.position + playerBody.right * sender.TextSpawnOffset.x + playerBody.right * textSpacing * characterCombo;
            charPosition.y += sender.TextSpawnOffset.y;
            TMP_Text newCharacter = Instantiate(textPrefab, charPosition, playerBody.rotation);
            newCharacter.text = character;

            characterCombo++;
            
            StartCoroutine(DeleteText(newCharacter));
        }
        
        characterCombo = 0;
        characters.Clear();
    }

    private IEnumerator DeleteText(TMP_Text textObj)
    {
        yield return new WaitForSeconds(characterLifetime);

        textObj.GetComponent<Rigidbody>().isKinematic = false;
        
        yield return new WaitForSeconds(characterLifetime);

        textObj.GetComponent<Collider>().enabled = false;
        Destroy(textObj.gameObject, characterLifetime);
    }
}
