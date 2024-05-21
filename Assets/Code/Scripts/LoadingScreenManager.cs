using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    //Singleton
    public static LoadingScreenManager instance;
    public GameObject loadingScreen;
    public static bool isLoading = false;
    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start() {
        Time.timeScale = 1;
    }

     public void SwitchtoScene(string sceneName)
    {
        if (!isLoading) // Check if a scene load operation is not already in progress
        {
            isLoading = true; // Set the flag to indicate that a scene load operation is starting
            loadingScreen.SetActive(true);
            StartCoroutine(SwitchtoSceneAsync(sceneName));
        }
        else
        {
            Debug.Log("A scene load operation is already in progress.");
        }
    }

    IEnumerator SwitchtoSceneAsync(string sceneName)
    {
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
        isLoading = false; // Reset the flag since the scene load operation has completed
    }
}
