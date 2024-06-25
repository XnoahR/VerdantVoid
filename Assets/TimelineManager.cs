using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector playableDirector; // Reference to the PlayableDirector
    private bool isPlayed;
    private GameObject cinematicScreen;

    void Start()
    {
        cinematicScreen = GameObject.Find("CinematicScreen");
        cinematicScreen.SetActive(false);
        // Automatically get the PlayableDirector component attached to the same GameObject
        playableDirector = GetComponent<PlayableDirector>();

        // Load the persistent flag from PlayerPrefs
        isPlayed = PlayerPrefs.GetInt("TimelinePlayed", 0) == 1;
        Debug.Log("Timeline played: " + isPlayed);

        // If the timeline has already been played, do not play it again
        if (!isPlayed && playableDirector != null)
        {
            PlayTimeline();
        }
    }

    private void PlayTimeline()
    {
        if (playableDirector != null)
        {
            playableDirector.Play();
            // Set the flag to true
            isPlayed = true;
            // Save the flag state to PlayerPrefs
            PlayerPrefs.SetInt("TimelinePlayed", 1);
            PlayerPrefs.Save();
        }
    }

    public void ResetTimelineFlag()
    {
        PlayerPrefs.SetInt("TimelinePlayed", 0);
        PlayerPrefs.Save();
    }
}
