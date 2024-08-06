using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VerdantVoid.Managers;


public class Opening : MonoBehaviour
{
     private void OnEnable() {
        LoadingScreenManager.instance.SwitchtoScene("C1S1_Bedroom");
        BacksoundManager.instance.PlayMusic("General");
    }
}
