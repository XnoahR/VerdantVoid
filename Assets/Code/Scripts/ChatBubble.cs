using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TMPro.TextMeshPro textMeshPro;
    private Transform playerTransform;
    private bool isTyping = false;

    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TMPro.TextMeshPro>();
    }

    public void Setup(List<InteractionObject.Interaction> interactions, Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        transform.Find("Background").gameObject.SetActive(true);
        transform.Find("Text").gameObject.SetActive(true);
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
        transform.Find("Background").gameObject.SetActive(false);
        transform.Find("Text").gameObject.SetActive(false);
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

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(3.85f, 3.4f); //magic number
        backgroundSpriteRenderer.size = textSize + padding;

        Vector3 offset = new Vector3(7f, 1.9f + textSize.y, 0f); //magic number
        textMeshPro.transform.localPosition = offset;

        textMeshPro.text = "";

        for (int i = 0; i <= interaction.paragraphs.Length; i++)
        {
            textMeshPro.text = interaction.paragraphs.Substring(0, i);
            yield return new WaitForSeconds(0.1f); // Adjust the delay to your liking
        }
        isTyping = false;
    }
}
