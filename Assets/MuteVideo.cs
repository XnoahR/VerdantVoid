using UnityEngine;
using UnityEngine.Video; // Required for accessing VideoPlayer

public class MuteVideo : MonoBehaviour
{
    void Start()
    {
        if (BacksoundManager.instance.audioSource.mute)
        {
            VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
            if (videoPlayer != null)
            {
                // Mute all audio tracks of the VideoPlayer
                for (ushort trackIndex = 0; trackIndex < videoPlayer.audioTrackCount; trackIndex++)
                {
                    videoPlayer.SetDirectAudioMute(trackIndex, true);
                }
            }
        }
    }
}
