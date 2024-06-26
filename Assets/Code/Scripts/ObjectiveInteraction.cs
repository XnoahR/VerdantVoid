using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveInteraction : Item
{

    public RequiredItems requiredItems;

    void Awake()
    {
        Debug.Log("TestInteract Awake");
    }

    public override void Interact()
    {

        if (GameplayMaster.IsObjectiveFulfilled(requiredItems))
        {
            Debug.Log("Objective Fulfilled");
        }
        else
        {
            Debug.Log($"Interacting with {interaction.objectName}");
            GameplayMaster.currentGameState = GameplayMaster.GameState.Interacting;
            Chat();
            Debug.Log("You need to find the required item first");
        }

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
