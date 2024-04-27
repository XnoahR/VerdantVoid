using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TMPro.TextMeshPro textMeshPro;
    //size of the text string

    private Transform playerTransform;
    private bool isTyping = false;
    public GameplayMaster gameplayMaster;
   
   private GameObject chatBubbleBackground;
    private GameObject chatBubbleText;


    private void Awake()
    {
        chatBubbleBackground = transform.Find("Background").gameObject;
        chatBubbleText = transform.Find("Text").gameObject;
        backgroundSpriteRenderer = chatBubbleBackground.GetComponent<SpriteRenderer>();
        textMeshPro = chatBubbleText.GetComponent<TMPro.TextMeshPro>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start() {
        gameplayMaster = GameObject.Find("Gameplay Master").GetComponent<GameplayMaster>();
        //size of background
        // chatBubbleBackground.transform.localScale = new Vector3(1, 1, 1);
        
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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && !isTyping);
        }
        chatBubbleBackground.gameObject.SetActive(false);
        chatBubbleText.gameObject.SetActive(false);
        gameplayMaster.currentGameState = GameplayMaster.GameState.Gameplay;
    }

    private IEnumerator Chat(InteractionObject.Interaction interaction)
    {
        isTyping = true;
        textMeshPro.text = interaction.paragraphs;
        textMeshPro.ForceMeshUpdate();
        gameObject.transform.position =
            interaction.speakerName != "Player"
                ? transform.parent.position
                : playerTransform.position;

        Vector2 textSize = textMeshPro.GetRenderedValues(false); //size of the text string
        Vector2 padding = new Vector2(3.85f, 3.4f); //magic number
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(7f, 1.9f + textSize.y, 0f); //magic number
        textMeshPro.transform.localPosition = offset;

        textMeshPro.text = "";

        for (int i = 0; i <= interaction.paragraphs.Length; i++)
        {
            textMeshPro.text = interaction.paragraphs.Substring(0, i);
            yield return new WaitForSeconds(0.025f);
        }
        isTyping = false;
    }
}
