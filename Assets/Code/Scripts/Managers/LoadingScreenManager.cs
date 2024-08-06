using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace VerdantVoid.Managers
{
    public class LoadingScreenManager : MonoBehaviour
    {
        //Singleton
        public static LoadingScreenManager instance;
        public GameObject loadingScreen;
        public Image fadeImage;
        public static bool isLoading = false;
        public bool isMainMenu = true;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start()
        {
            Time.timeScale = 1;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
            fadeImage.gameObject.SetActive(false);
        }

        public void SwitchtoScene(string sceneName)
        {
            if (!isLoading) // Check if a scene load operation is not already in progress
            {
                if (isMainMenu)
                {
                    isLoading = true; // Set the flag to indicate that a scene load operation is starting
                    isMainMenu = false;
                    StartCoroutine(SwitchtoSceneRoutine(sceneName));
                }
                else
                {
                    isLoading = true; // Set the flag to indicate that a scene load operation is starting
                    loadingScreen.SetActive(true);
                    StartCoroutine(SwitchtoSceneAsync(sceneName));
                }
            }
            else
            {
                Debug.Log("A scene load operation is already in progress.");
            }
        }

        private IEnumerator SwitchtoSceneRoutine(string sceneName)
        {
            BacksoundManager.instance.StartCoroutine("FadeOut");
            // Start fade-in and wait for it to complete
            yield return StartCoroutine(FadeIn());

            // Activate loading screen
            loadingScreen.SetActive(true);

            // Start loading the scene
            yield return StartCoroutine(SwitchtoSceneAsync(sceneName));

            // Deactivate loading screen and start fade-out
            loadingScreen.SetActive(false);
            BacksoundManager.instance.PlayMusic("General");
            yield return StartCoroutine(FadeOut());

            // Reset the flag since the scene load operation has completed
            isLoading = false;
        }
        IEnumerator SwitchtoSceneAsync(string sceneName)
        {
            BacksoundManager.instance.StartCoroutine("FadeOut");
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            // if(SceneManager.GetActiveScene().name == "MainMenu"){
            //     //Destroy the loading screen when the main menu scene is loaded
            //     Destroy(this.gameObject);
            // }
            yield return new WaitForSeconds(1);
            loadingScreen.SetActive(false);
            BacksoundManager.instance.StartCoroutine("FadeIn");
            isLoading = false; // Reset the flag since the scene load operation has completed
        }

        IEnumerator FadeIn()
        {
            fadeImage.gameObject.SetActive(true);
            //3 seconds fade in
            for (float i = 0; i <= 1; i += Time.deltaTime / 3)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, i);
                yield return null;
            }

        }

        IEnumerator FadeOut()
        {
            //3 seconds fade out
            for (float i = 1; i >= 0; i -= Time.deltaTime / 3)
            {
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, i);
                yield return null;
            }
            fadeImage.gameObject.SetActive(false);
        }

    }
}