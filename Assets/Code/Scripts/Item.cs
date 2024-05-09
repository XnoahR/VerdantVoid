using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        chatBubbleGO = transform.Find("ChatBubble").gameObject;
        playerTransform = GameplayMaster.player.transform;
        interactSign = transform.Find("InteractSign").GetComponent<SpriteRenderer>();
        chatBubble = chatBubbleGO.GetComponent<ChatBubble>();
    }

    public virtual void Interact()
    {
        Debug.Log("Item Interacted");
    }

    public virtual void Chat()
    {
        Debug.Log("Item Chatted");
    }

    public bool PlayerNearby()
    {
        bool isPlayerNearby =
            Mathf.Abs(playerTransform.position.x - transform.position.x) <= INTERACT_DISTANCE;

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
        PlayerNearby();

        if (
            PlayerNearby()
            && Input.GetKeyDown(KeyCode.E)
            && GameplayMaster.currentGameState == GameplayMaster.GameState.Gameplay
        )
        {
            GameplayMaster.currentGameState = GameplayMaster.GameState.Interacting;
            Interact();
        }
    }
}
