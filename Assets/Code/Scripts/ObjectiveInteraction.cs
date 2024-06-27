using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveInteraction : Item
{
    public ObjectiveObject objective;
    public InteractionObject objectiveChat;

    void Awake()
    {
        Debug.Log("TestInteract Awake");
    }

    
    public override void Interact()
    {
        if (ObjectiveManager.instance.IsObjectiveFulfilled(objective))
        {
            GameplayMaster.currentGameState = GameplayMaster.GameState.Interacting;
            ObjectiveChat();
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

    public void ObjectiveChat(){
        Debug.Log($"Chatting with {interaction.objectName}");
        chatBubble.Setup(objectiveChat.interactions);
        chatBubble.OnInteractionComplete += OnChatObjectiveComplete;

    }

    private void OnChatComplete()
    {
        chatBubble.OnInteractionComplete -= OnChatComplete;
        GameplayMaster.currentGameState = GameplayMaster.GameState.Gameplay;
    }

    private void OnChatObjectiveComplete(){
        chatBubble.OnInteractionComplete -= OnChatObjectiveComplete;
        ObjectiveManager.instance.CheckObjectives();  
    }
}
