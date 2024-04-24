using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeArea : MonoBehaviour
{
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
            SceneManager.LoadScene("SceneTest 1");
        }
    }

    public bool PlayerNearby()
    {
        return Mathf.Abs(playerTransform.position.x - transform.position.x) <= INTERACT_DISTANCE;
    }
}
