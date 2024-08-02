using UnityEngine;
using UnityEngine.Playables;


namespace Assets.Code.Scripts.Managers
{
    public class TimelineManager : MonoBehaviour
    {
        private PlayableDirector playableDirector; // Reference to the PlayableDirector
        private bool isPlayed;
        private GameObject cinematicScreen;
        public TimelineObject timelineObject;
        private TimelineObject.TimelineData currentTimelineObject;

        void Start()
        {
            cinematicScreen = GameObject.Find("CinematicScreen");
            // cinematicScreen.SetActive(false);
            // Automatically get the PlayableDirector component attached to the same GameObject
            playableDirector = GetComponent<PlayableDirector>();
            foreach (TimelineObject.TimelineData timeline in timelineObject.timelines)
            {
                if (timeline.stage == GameplayMaster.currentStage)
                {
                    currentTimelineObject = timeline;
                    break;
                }
            }

            // Load the persistent flag from PlayerPrefs
            playableDirector.playableAsset = currentTimelineObject.playable;
            isPlayed = PlayerPrefs.GetInt(currentTimelineObject.timelineKey, 0) == 1;
            Debug.Log("Timeline played: " + isPlayed);

        }

        void Update()
        {
            // If the timeline is not played and the space key is pressed, play the timeline
            if (!isPlayed && Input.GetKeyDown(KeyCode.T))
            {
                currentTimelineObject.isTrigger = true;
            }

            CheckTrigger();
        }

        private void PlayTimeline()
        {
            if (playableDirector != null)
            {
                playableDirector.Play();
                // Set the flag to true
                isPlayed = true;
                // Save the flag state to PlayerPrefs
                PlayerPrefs.SetInt(currentTimelineObject.timelineKey, 1);
                PlayerPrefs.Save();
            }
        }

        public void ResetTimelineFlag()
        {
            PlayerPrefs.SetInt("TimelinePlayed", 0);
            PlayerPrefs.Save();
        }

        public void CheckTrigger()
        {
            if (!isPlayed && playableDirector != null && currentTimelineObject.isTrigger)
            {
                PlayTimeline();
            }
        }
    }
}