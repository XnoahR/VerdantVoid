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
    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SwitchtoScene(string sceneName){
        loadingScreen.SetActive(true);
        StartCoroutine(SwitchtoSceneAsync(sceneName));
    }

    IEnumerator SwitchtoSceneAsync(string sceneName){
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while(!asyncLoad.isDone){
            yield return null;
        }
        yield return new WaitForSeconds(1);
        loadingScreen.SetActive(false);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
