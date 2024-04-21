using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IInteractable
{
    private Transform playerTransform;
    protected SpriteRenderer spriteRenderer;

    private bool wasPlayerNearby = false; // to check if player was nearby in the last frame, later remove this

    [SerializeField]
    protected float INTERACT_DISTANCE;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Interact()
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

        spriteRenderer.color = isPlayerNearby ? Color.green : Color.red;
        wasPlayerNearby = isPlayerNearby;

        return isPlayerNearby;
    }
}
