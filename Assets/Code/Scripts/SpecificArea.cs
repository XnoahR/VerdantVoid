using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecificArea : MonoBehaviour, IInteractable
{
    private SpriteRenderer interactSign;
    private Transform playerTransform;

    [SerializeField]
    private float INTERACT_DISTANCE;

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private string purposedObjectName;

    private bool wasPlayerNearby = false; // to check if player was nearby in the last frame, later remove this

    private void Start()
    {
        playerTransform = GameplayMaster.player.transform;
        interactSign = transform.Find("InteractSign").GetComponent<SpriteRenderer>();
        INTERACT_DISTANCE = 2f;
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

    public void Interact()
    {
        Debug.Log("Interacting to Specific Area");
        LevelLoader.objectName = purposedObjectName;
        GameplayMaster.currentGameState = GameplayMaster.GameState.Interacting;
        // SceneManager.LoadScene(sceneName);
        LoadingScreenManager.instance.SwitchtoScene(sceneName);
    }
}
