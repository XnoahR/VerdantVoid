using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

[System.Serializable]
public class ChatBubblePlayableAsset : PlayableAsset
{
    public InteractionObject interactionObject;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<ChatBubblePlayableBehaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.interactionObject = interactionObject;
        return playable;
    }
}
