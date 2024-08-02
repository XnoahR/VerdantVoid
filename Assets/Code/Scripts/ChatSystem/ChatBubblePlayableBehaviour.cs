using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

namespace Assets.Code.Scripts.ChatSystem
{

    public class ChatBubblePlayableBehaviour : PlayableBehaviour
    {
        public InteractionObject interactionObject;
        private bool interactionComplete;
        private bool isChat;
        private PlayableDirector director;
        private ChatBubble chatBubble;
        public double timelineTimeToPause; // New field to store the timeline time to pause
        private bool paused; // Flag to ensure the director is paused only once
        private bool isDone = false;

        public override void OnPlayableCreate(Playable playable)
        {
            base.OnPlayableCreate(playable);
            isChat = false;
            interactionComplete = false;
            director = (playable.GetGraph().GetResolver() as PlayableDirector);
            paused = false;
            isDone = false;
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (interactionObject == null) return;

            GameObject chatBubbleParent = GameObject.Find("ChatBubbleParent"); // Ensure you have a parent object
            chatBubble = chatBubbleParent.GetComponentInChildren<ChatBubble>();

            if (chatBubble != null && isDone == false)
            {
                isDone = true;
                chatBubble.Setup(interactionObject.interactions);
                chatBubble.OnInteractionComplete += OnInteractionComplete;
                isChat = true;
            }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // Debug.Log("Processing frame");
            if (isChat)
            {
                if (!interactionComplete)
                {
                    director.Pause();
                    Debug.Log("Paused at timeline time: " + timelineTimeToPause);
                    paused = true;
                }
            }
        }

        private void OnInteractionComplete()
        {
            interactionComplete = true;
            chatBubble.OnInteractionComplete -= OnInteractionComplete;
            isChat = false;

            director.Resume();
            Debug.Log("Interaction complete");
        }
    }
}