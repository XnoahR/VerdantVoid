using System.Collections.Generic;
using UnityEngine;

public class SpeakerManager : MonoBehaviour
{
    public static SpeakerManager Instance { get; private set; }
    private Dictionary<string, Transform> speakerTransforms;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of SpeakerManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSpeakerTransforms();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSpeakerTransforms()
    {
        speakerTransforms = new Dictionary<string, Transform>
        {
            { "Player", GameObject.Find("Player").transform },
            {"father", GameObject.Find("father").transform},
            {"box", GameObject.Find("box").transform}
            // Add other speakers as needed
            // { "SpeakerName", GameObject.Find("SpeakerObjectName").transform },
        };
    }

    public Transform GetSpeakerTransform(string speakerName)
    {
        if (speakerTransforms.TryGetValue(speakerName, out Transform speakerTransform))
        {
            return speakerTransform;
        }
        else
        {
            Debug.LogWarning($"Speaker {speakerName} not found in SpeakerManager!");
            return null;
        }
    }

    public void RegisterSpeaker(string speakerName, Transform speakerTransform)
    {
        if (!speakerTransforms.ContainsKey(speakerName))
        {
            speakerTransforms.Add(speakerName, speakerTransform);
        }
        else
        {
            Debug.LogWarning($"Speaker {speakerName} is already registered in SpeakerManager!");
        }
    }
}
