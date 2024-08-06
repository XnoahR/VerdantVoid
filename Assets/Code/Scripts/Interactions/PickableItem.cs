using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VerdantVoid.Code.Scripts.Managers;

public class PickableItem : Item
{
    public bool isPickedUp = false;    
    private string itemKey;

    protected override void Start()
    {
        base.Start();
        itemKey = interaction.name;
        isPickedUp = PlayerPrefs.GetInt(itemKey, 0) == 1;
        Debug.Log($"Item {itemKey} isPickedUp: {isPickedUp}");
        gameObject.SetActive(!isPickedUp);
    }

    public override void Interact()
    {
        Debug.Log($"Interacting with {interaction.objectName}");
        GameplayMaster.currentGameState = GameplayMaster.GameState.Interacting;
        Chat();
    }

    public override void Chat()
    {
        Debug.Log($"Chatting with {interaction.objectName}");
        chatBubble.Setup(interaction.interactions);
        chatBubble.OnInteractionComplete += OnChatComplete;
    }

    private void OnChatComplete()
    {
        chatBubble.OnInteractionComplete -= OnChatComplete;
        GameplayMaster.inventory.Add(interaction.objectName);
        Debug.Log("Inventory: " + string.Join(", ", GameplayMaster.inventory));
        isPickedUp = true;
        PlayerPrefs.SetInt(itemKey, 1); // Store the state in PlayerPrefs
        PlayerPrefs.Save(); 
        transform.gameObject.SetActive(false);
        //List Inventory
        foreach (string item in GameplayMaster.inventory){
            Debug.Log(item);
        }
        GameplayMaster.currentGameState = GameplayMaster.GameState.Gameplay;
    }
}
