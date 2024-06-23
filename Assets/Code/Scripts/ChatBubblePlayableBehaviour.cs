using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class ChatBubblePlayableBehaviour : PlayableBehaviour
{
    public InteractionObject interactionObject;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (interactionObject == null) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ChatBubble chatBubble = player.GetComponentInChildren<ChatBubble>();

        if (chatBubble != null)
        {
            chatBubble.Setup(interactionObject.interactions);
        }
    }
}
