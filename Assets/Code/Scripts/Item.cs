using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    private Transform playerTransform;
    protected SpriteRenderer interactSign;

    private bool wasPlayerNearby = false; // to check if player was nearby in the last frame, later remove this

    [SerializeField]
    protected float INTERACT_DISTANCE;
    public InteractionObject interaction;

    private void Awake()
    {
        interactSign = transform.GetChild(0).GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public virtual void Interact()
    {
        Debug.Log("Item Interacted");
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


   private void Update() {
        PlayerNearby();

        if (PlayerNearby() && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
   }
}
