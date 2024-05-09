using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static ChangeArea.Position ChangeAreaPosition = ChangeArea.Position.None;

    void Start()
    {
        Debug.Log(ChangeAreaPosition);

        if (ChangeAreaPosition == ChangeArea.Position.L)
        {
            GameplayMaster.player.transform.position =
                GameObject.Find("ChangeAreaL").transform.position + new Vector3(2.5f, 0, 0);
        }
        else if (ChangeAreaPosition == ChangeArea.Position.R)
        {
            GameplayMaster.player.transform.position =
                GameObject.Find("ChangeAreaR").transform.position + new Vector3(-2.5f, 0, 0);
        }
    }
}
