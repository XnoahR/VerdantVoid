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

    private Transform playerTransform;
    private const float INTERACT_DISTANCE = 1.5f;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (PlayerNearby())
        {
            GameplayMaster.ChangeAreaPosition = purposedPosition;
            SceneManager.LoadScene(sceneName);
        }
    }

    public bool PlayerNearby()
    {
        return Mathf.Abs(playerTransform.position.x - transform.position.x) <= INTERACT_DISTANCE;
    }
}
