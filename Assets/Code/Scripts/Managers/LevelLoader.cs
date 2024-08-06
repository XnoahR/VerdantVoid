using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VerdantVoid.Code.Scripts.Managers
{
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
                // Debug.Log(objectName);
                GameplayMaster.player.transform.position = GameObject
                    .Find(objectName)
                    .transform.position;
            }
            else if (changeAreaPosition != ChangeArea.Position.None)
            {
                Debug.Log("WHY ARE YOU HERE");
            }

            float yGround = GameObject.Find("Ground").transform.position.y;
            float yGroundSize = GameObject.Find("Ground").GetComponent<SpriteRenderer>().bounds.size.y;
            Debug.Log($"Ground position: {yGround}, Ground size: {yGroundSize}");
            GameplayMaster.player.transform.position = new Vector3(
                GameplayMaster.player.transform.position.x,
                yGround + 2 * yGroundSize,
                0
            );
            // Debug.Log($"New Player position: {GameplayMaster.player.transform.position.x} {GameplayMaster.player.transform.position.y} {GameplayMaster.player.transform.position.z}");

            //set to after asyn load
            // Debug.Log(changeAreaPosition);
            // Debug.Log("XLevelLoader: " + changeAreaPosition + " " + objectName);
            changeAreaPosition = ChangeArea.Position.None;
            objectName = null;
        }
    }
}