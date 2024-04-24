using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private TMPro.TextMeshPro textMeshPro;
    private Transform playerTransform;

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
            Chat(interaction);
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        transform.Find("Background").gameObject.SetActive(false);
        transform.Find("Text").gameObject.SetActive(false);
    }

    private void Chat(InteractionObject.Interaction interaction)
    {
        if (interaction.speakerName != "Player")
        {
            gameObject.transform.position = transform.parent.position;
            textMeshPro.text = interaction.paragraphs;
            textMeshPro.ForceMeshUpdate();

            Vector2 textSize = textMeshPro.GetRenderedValues(false);
            Vector2 padding = new Vector2(3.85f, 3.4f); //magic number
            backgroundSpriteRenderer.size = textSize + padding;

            Vector3 offset = new Vector3(7f, 1.9f, 0); //magic number
            textMeshPro.transform.localPosition = offset;
        }
        else
        {
            gameObject.transform.position = playerTransform.position;
            textMeshPro.text = "HAIZK";
            textMeshPro.ForceMeshUpdate();

            Vector2 textSize = textMeshPro.GetRenderedValues(false);
            Vector2 padding = new Vector2(3.85f, 3.4f); //magic number
            backgroundSpriteRenderer.size = textSize + padding;

            Vector3 offset = new Vector3(7f, 1.9f, 0); //magic number
            Debug.Log("Player is speaking");
        }
    }
}
