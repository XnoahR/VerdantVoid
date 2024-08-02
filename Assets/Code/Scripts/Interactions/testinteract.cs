using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Scripts.Managers;


public class TestInteract : Item
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
        GameplayMaster.currentGameState = GameplayMaster.GameState.Gameplay;
    }
}