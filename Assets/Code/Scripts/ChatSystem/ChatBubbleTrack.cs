using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Assets.Code.Scripts.ChatSystem
{
    [TrackColor(0.736f, 0.214f, 0.381f)]
    [TrackClipType(typeof(ChatBubblePlayableAsset))]
    [TrackBindingType(typeof(GameObject))]
    public class ChatBubbleTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            Debug.Log("Creating ChatBubbleTrack");
            return ScriptPlayable<ChatBubblePlayableBehaviour>.Create(graph, inputCount);

        }
    }
}
