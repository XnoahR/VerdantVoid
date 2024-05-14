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
            Debug.Log("ChangeAreaL");
        }
        else if (changeAreaPosition == ChangeArea.Position.R)
        {
            GameplayMaster.player.transform.position =
                GameObject.Find("ChangeAreaR").transform.position + new Vector3(-2.5f, 0, 0);

            Debug.Log("ChangeAreaR");
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

        GameplayMaster.player.transform.position = new Vector3(
            GameplayMaster.player.transform.position.x,
            -0.8997308f, //magic number
            0
        );
        Debug.Log($"New Player position: {GameplayMaster.player.transform.position.x} {GameplayMaster.player.transform.position.y} {GameplayMaster.player.transform.position.z}");

        //set to after asyn load
        Debug.Log(changeAreaPosition);
        changeAreaPosition = ChangeArea.Position.None;
        Debug.Log("XLevelLoader: " + changeAreaPosition + " " + objectName);
        objectName = null;

        
    }
}
