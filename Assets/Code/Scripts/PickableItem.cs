using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Item
{
    

    void Awake()
    {
        Debug.Log("TestInteract Awake");
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
        transform.gameObject.SetActive(false);
        GameplayMaster.inventory.Add(interaction.objectName);
        Debug.Log("Inventory: " + string.Join(", ", GameplayMaster.inventory));
        //List Inventory
        foreach (string item in GameplayMaster.inventory){
            Debug.Log(item);
        }
        GameplayMaster.currentGameState = GameplayMaster.GameState.Gameplay;
    }
}
