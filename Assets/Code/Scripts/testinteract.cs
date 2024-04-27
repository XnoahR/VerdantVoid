using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : Item
{
    // Start is called before the first frame update
    // public GameplayMaster gameplayMaster;
    void Start()
    {
        INTERACT_DISTANCE = 2f;
        // gameplayMaster = GameObject.Find("Gameplay Master").GetComponent<GameplayMaster>();
    }

    public override void Interact()
    {
        Debug.Log($"Interacting with {interaction.objectName}");
        gameplayMaster.currentGameState = GameplayMaster.GameState.Interacting;
        Chat();
    }

    public override void Chat()
    {
        Debug.Log($"Chatting with {interaction.objectName}");
        chatBubble.Setup(interaction.interactions);
    }
}
