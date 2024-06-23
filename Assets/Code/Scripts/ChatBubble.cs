using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TMPro.TextMeshPro textMeshPro;

    private Transform playerTransform;
    private bool isTyping = false;

    private GameObject chatBubbleBackground;
    private GameObject chatBubbleText;
    private Camera mainCamera;

    private void Awake()
    {
        chatBubbleBackground = transform.Find("Background").gameObject;
        chatBubbleText = transform.Find("Text").gameObject;
        backgroundSpriteRenderer = chatBubbleBackground.GetComponent<SpriteRenderer>();
        textMeshPro = chatBubbleText.GetComponent<TMPro.TextMeshPro>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        mainCamera = Camera.main;
    }

    public void Setup(List<InteractionObject.Interaction> interactions)
    {
        chatBubbleBackground.gameObject.SetActive(true);
        chatBubbleText.gameObject.SetActive(true);
        StartCoroutine(ChatSequence(interactions));
    }

    private IEnumerator ChatSequence(List<InteractionObject.Interaction> interactions)
    {
        foreach (InteractionObject.Interaction interaction in interactions)
        {
            StartCoroutine(Chat(interaction));
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => !isTyping && Input.GetKeyDown(KeyCode.Space));
        }
        chatBubbleBackground.gameObject.SetActive(false);
        chatBubbleText.gameObject.SetActive(false);
        GameplayMaster.currentGameState = GameplayMaster.GameState.Gameplay;
        // Update game state or any other necessary logic
    }

    private IEnumerator Chat(InteractionObject.Interaction interaction)
    {
        isTyping = true;
        textMeshPro.text = interaction.paragraphs;
        textMeshPro.ForceMeshUpdate();

        // Set the position of the chat bubble
        if (interaction.speakerName != "Player")
        {
            transform.position = transform.parent.position;
        }
        else
        {
            transform.position = playerTransform.position;
        }

        Vector3 playerScreenPosition = mainCamera.WorldToViewportPoint(transform.position);
        Debug.Log(playerScreenPosition.x);
        if (playerScreenPosition.x < 0.5f) // Adjust this value according to your preference
        {
            // Player is close to the left border of the camera
            backgroundSpriteRenderer.transform.localScale = new Vector3(Mathf.Abs(backgroundSpriteRenderer.transform.localScale.x), backgroundSpriteRenderer.transform.localScale.y, backgroundSpriteRenderer.transform.localScale.z);
        }
        else
        {
            // Player is not close to the left border of the camera
            backgroundSpriteRenderer.transform.localScale = new Vector3(-Mathf.Abs(backgroundSpriteRenderer.transform.localScale.x), backgroundSpriteRenderer.transform.localScale.y, backgroundSpriteRenderer.transform.localScale.z);
        }

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(1.45f, 3.2f);
        backgroundSpriteRenderer.size = textSize + padding;
        Debug.Log(backgroundSpriteRenderer.size.x);

        Vector3 offset = playerScreenPosition.x < 0.5f ? new Vector3(5.7f, 1.8f + textSize.y, 0f) : new Vector3(5.7f - backgroundSpriteRenderer.size.x, 1.8f + textSize.y, 0f);
        textMeshPro.transform.localPosition = offset;

        // Ensure the chat bubble stays within the camera view
        Vector3 bubblePosition = transform.position;
        bubblePosition = ClampToCameraView(bubblePosition, backgroundSpriteRenderer.size / 2);
        transform.position = bubblePosition;

        textMeshPro.text = "";

        for (int i = 0; i <= interaction.paragraphs.Length; i++)
        {
            textMeshPro.text = interaction.paragraphs.Substring(0, i);
            yield return new WaitForSeconds(0.025f);
        }
        isTyping = false;
    }

    private Vector3 ClampToCameraView(Vector3 position, Vector2 size)
    {
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(position);
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, size.x / Screen.width, 1 - size.x / Screen.width);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, size.y / Screen.height, 1 - size.y / Screen.height);
        return mainCamera.ViewportToWorldPoint(viewportPosition);
    }
}
