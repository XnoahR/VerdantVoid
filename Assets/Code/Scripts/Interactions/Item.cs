using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VerdantVoid.Code.Scripts.Managers;
using VerdantVoid.Code.Scripts.ChatSystem;

public abstract class Item : MonoBehaviour, IInteractable, IChatable
{
    protected Transform playerTransform;
    protected SpriteRenderer interactSign;
    private GameObject chatBubbleGO;
    protected ChatBubble chatBubble;

    private bool wasPlayerNearby = false; // to check if player was nearby in the last frame, later remove this

    [SerializeField]
    protected float INTERACT_DISTANCE;
    public InteractionObject interaction;

    protected virtual void Start()
    {
         Debug.Log("Item Start");
        chatBubbleGO = transform.Find("ChatBubble").gameObject;
        playerTransform = GameplayMaster.player.transform;
        Debug.Log("anjay" + playerTransform);
        interactSign = transform.Find("InteractSign").GetComponent<SpriteRenderer>();
        chatBubble = chatBubbleGO.GetComponent<ChatBubble>();

        AdditionalCondition();
    }

    protected virtual void AdditionalCondition(){
       Debug.Log("Item Additional Condition");
    }

    public virtual void Interact()
    {
        Debug.Log("Item Interacted");
        GameplayMaster.currentGameState = GameplayMaster.GameState.Interacting;
    }

    public virtual void Chat()
    {
        Debug.Log("Item Chatted");
    }

    public bool PlayerNearby()
    {
        bool isPlayerNearby =
            Mathf.Abs(playerTransform.position.x - transform.position.x) <= INTERACT_DISTANCE && GameplayMaster.currentGameState == GameplayMaster.GameState.Gameplay;

        if (isPlayerNearby && !wasPlayerNearby)
        {
            Debug.Log(gameObject.name + ": Player is nearby");
        }
        else if (!isPlayerNearby && wasPlayerNearby)
        {
            Debug.Log(gameObject.name + ": Player is not nearby anymore");
        }

        interactSign.enabled = isPlayerNearby;
        wasPlayerNearby = isPlayerNearby;

        return isPlayerNearby;
    }

    private void Update()
    {
        // Debug.Log(playerTransform);
        PlayerNearby();

        if (
            PlayerNearby()
            && Input.GetKeyDown(KeyCode.E)
            && GameplayMaster.currentGameState == GameplayMaster.GameState.Gameplay
        )
        {
            Interact();
        }
    }
}
