using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeArea : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public enum Position
    {
        L,
        R,
        None
    }

    [SerializeField]
    private Position purposedPosition;

    private const float INTERACT_DISTANCE = 1.5f;

    private void Update()
    {
        if (PlayerNearby())
        {
            LevelLoader.changeAreaPosition = purposedPosition;
            // SceneManager.LoadScene(sceneName);
            LoadingScreenManager.instance.SwitchtoScene(sceneName);
        }
    }

    public bool PlayerNearby()
    {
        return Mathf.Abs(GameplayMaster.player.transform.position.x - transform.position.x)
            <= INTERACT_DISTANCE;
    }
}
