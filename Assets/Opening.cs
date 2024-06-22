using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Opening : MonoBehaviour
{
     private void OnEnable() {
        LoadingScreenManager.instance.SwitchtoScene("Chapter1_Bedroom");
    }
}
