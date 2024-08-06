using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VerdantVoid.Code.Scripts.Managers;


public class Opening : MonoBehaviour
{
     private void OnEnable() {
        LoadingScreenManager.instance.SwitchtoScene("C1S1_Bedroom");
        BacksoundManager.instance.PlayMusic("General");
    }
}
