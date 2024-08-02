using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Code.Scripts.Managers
{
    public class BacksoundManager : MonoBehaviour
    {
        public static BacksoundManager instance;

        public AudioSource audioSource;

        [System.Serializable]
        public class AudioEntry
        {
            public string name;
            public AudioClip clip;
        }

        public TextMeshProUGUI text;

        public List<AudioEntry> audioList = new List<AudioEntry>();

        void Awake()
        {
            // Ensure that there is only one instance of BacksoundManager
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // This will persist the object across scenes
            }
            else
            {
                Destroy(gameObject); // Destroy duplicate instances
            }

            audioSource = GetComponent<AudioSource>();
            PlayMusic("MM");
        }

        // Play background music by name
        public void PlayMusic(string name)
        {
            AudioEntry entry = audioList.Find(x => x.name == name);
            if (entry != null && audioSource.clip != entry.clip)
            {
                audioSource.clip = entry.clip;
                audioSource.loop = true;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Audio clip not found or already playing.");
            }
        }

        // Stop background music
        public void StopMusic()
        {
            audioSource.Stop();
        }

        // Pause background music
        public void PauseMusic()
        {
            audioSource.Pause();
        }

        // Resume background music
        public void ResumeMusic()
        {
            audioSource.UnPause();
        }

        public IEnumerator FadeOut()
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime;
                yield return null;
            }
            audioSource.Pause();
        }

        public IEnumerator FadeIn()
        {
            audioSource.Play();
            while (audioSource.volume < 0.25f)
            {
                audioSource.volume += Time.deltaTime;
                yield return null;
            }
        }

        public void ToggleSound()
        {
            text = GameObject.Find("SoundText").GetComponent<TextMeshProUGUI>();
            if (audioSource.mute)
            {
                audioSource.mute = false;
                text.text = "O";
            }
            else
            {
                audioSource.mute = true;
                text.text = "";
            }
        }
    }
}