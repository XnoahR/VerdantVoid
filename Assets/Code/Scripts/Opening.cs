using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Code.Scripts.Managers;


public class Opening : MonoBehaviour
{
     private void OnEnable() {
        LoadingScreenManager.instance.SwitchtoScene("C1S1_Bedroom");
        BacksoundManager.instance.PlayMusic("General");
    }
}
