using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class ChatBubblePlayableBehaviour : PlayableBehaviour
{
    public InteractionObject interactionObject;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (interactionObject == null) return;

        GameObject chatBubbleParent = GameObject.Find("ChatBubbleParent"); // Ensure you have a parent object
        ChatBubble chatBubble = chatBubbleParent.GetComponentInChildren<ChatBubble>();

        if (chatBubble != null)
        {
            chatBubble.Setup(interactionObject.interactions);
        }
    }
}
