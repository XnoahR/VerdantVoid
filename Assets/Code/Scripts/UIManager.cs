using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

 
    public void PlayGame()
    {
        //SceneManager.LoadScene("GameScene");
        LoadingScreenManager.instance.SwitchtoScene("Opening");
    }

    // onclick event for exit button
    public void ExitGame()
    {
        Application.Quit();
    }    

}
