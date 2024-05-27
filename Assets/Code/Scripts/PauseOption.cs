using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class PauseOption : MonoBehaviour
{
    // Start is called before the first frame update
    GameplayMaster GameplayMaster;
    void Start()
    {
        GameplayMaster = GameObject.Find("Gameplay Master").GetComponent<GameplayMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        GameplayMaster.RestoreState();
    }

    public void ExitGame()
    {
         GameplayMaster.RestoreState();
        Debug.Log(Time.timeScale);
       LoadingScreenManager.instance.SwitchtoScene("MainMenu");
       LoadingScreenManager.instance.isMainMenu = true;
    }
}
