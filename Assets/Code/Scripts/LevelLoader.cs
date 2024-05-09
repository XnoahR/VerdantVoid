using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static ChangeArea.Position changeAreaPosition = ChangeArea.Position.None;
    public static string objectName = null;

    void Start()
    {
        Debug.Log("LevelLoader: " + changeAreaPosition + " " + objectName);

        if (changeAreaPosition == ChangeArea.Position.L)
        {
            GameplayMaster.player.transform.position =
                GameObject.Find("ChangeAreaL").transform.position + new Vector3(2.5f, 0, 0);
        }
        else if (changeAreaPosition == ChangeArea.Position.R)
        {
            GameplayMaster.player.transform.position =
                GameObject.Find("ChangeAreaR").transform.position + new Vector3(-2.5f, 0, 0);
        }
        else if (objectName != null)
        {
            Debug.Log(objectName);
            GameplayMaster.player.transform.position = GameObject
                .Find(objectName)
                .transform.position;
        }
        else if (changeAreaPosition != ChangeArea.Position.None)
        {
            Debug.Log("WHY ARE YOU HERE");
        }

        changeAreaPosition = ChangeArea.Position.None;
        objectName = null;
    }
}