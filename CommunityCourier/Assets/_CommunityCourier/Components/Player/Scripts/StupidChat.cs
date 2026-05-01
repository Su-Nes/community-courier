using System.Collections;
using System.Collections.Generic;
using PurrNet;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class StupidChat : NetworkBehaviour
{
    [SerializeField] private PackageManager packageManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform playerBody;
    [SerializeField] private TMP_Text textPrefab;
    [SerializeField] private float textSpacing = .2f;
    [SerializeField] private Vector3 textSpawnOffset;
    public Vector3 TextSpawnOffset => textSpawnOffset;

    [SerializeField] private float characterComboTime = 1f, characterLifetime = 3f;
    private float comboTimer, timeAtLastButtonPress;
    private int characterCombo;

    private bool chatEnabled;
    
    private void Update()
    {
        if (InputManager.instance.inputActionAsset.FindAction("Submit").WasCompletedThisFrame())
            ToggleChatMode();

        HandleCharacterCombo();

        if (!chatEnabled)
            return;
        
        //MonitorKeyboard();
    }

    private void OnGUI()
    {
        if (!chatEnabled || !isOwner)
            return;
        
        Event e = Event.current;
        if (e.isKey)
        {
            string characterToType = e.character.ToString();
                
            DisplayCharacter(characterToType, this);
        }
    }

    private void HandleCharacterCombo()
    {
        if (characterCombo > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer >= characterComboTime)
            {
                characterCombo = 0;
                comboTimer = 0;
            }
        }
    }

    [ObserversRpc]
    private void DisplayCharacter(string character, StupidChat sender)
    {
        Vector3 charPosition = sender.transform.position + sender.TextSpawnOffset + playerBody.right * textSpacing * characterCombo;
        TMP_Text newCharacter = Instantiate(textPrefab, charPosition, playerBody.rotation);
        newCharacter.text = character;

        characterCombo++;
        comboTimer = 0f;
        
        StartCoroutine(DeleteText(newCharacter));
    }

    private void ToggleChatMode()
    {
        chatEnabled = !chatEnabled;
        
        playerController.SetActivity(!chatEnabled);
        packageManager.enabled = !chatEnabled;
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
