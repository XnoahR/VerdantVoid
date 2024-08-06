using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;


namespace VerdantVoid.ChatSystem
{
    [System.Serializable]
    public class ChatBubblePlayableAsset : PlayableAsset
    {
        public InteractionObject interactionObject;
        public double timelineTimeToPause; // New field to specify the timeline time to pause

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<ChatBubblePlayableBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.interactionObject = interactionObject;
            behaviour.timelineTimeToPause = timelineTimeToPause; // Pass timeline time to behaviour
            return playable;
        }
    }
}