using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody2D rb;
    private float PLAYER_SPEED;

    [SerializeField]
    float WALK_SPEED = 2f;

    [SerializeField]
    float SPRINT_SPEED = 5f;
    private bool isSprint;
    private GameplayMaster gameplayMaster;
    private bool canMove;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameplayMaster = GameObject.Find("Gameplay Master").GetComponent<GameplayMaster>();
    }

    void Start()
    {
        PLAYER_SPEED = WALK_SPEED;
    }

    void Update()
    {
        if (gameplayMaster.currentGameState == GameplayMaster.GameState.Gameplay)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    void FixedUpdate()
    {
        // Debug.Log(canMove);

        if (canMove)
        {
            PlayerMovement();
        }
    }

    void PlayerMovement()
    {
        // Debug.Log("Player Movement");
        float horizontalMove = Input.GetAxis("Horizontal");
        isSprint = Input.GetKey(KeyCode.LeftShift) ? true : false;
        PLAYER_SPEED = isSprint ? SPRINT_SPEED : WALK_SPEED;
        rb.velocity = new Vector2(horizontalMove * PLAYER_SPEED, rb.velocity.y);
    }
}
