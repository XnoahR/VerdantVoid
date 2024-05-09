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
    public bool facingRight = true;

    
    [Header("Player Animation")]

    public Animator animator;
    public bool isWalk;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameplayMaster = GameObject.Find("Gameplay Master").GetComponent<GameplayMaster>();
        animator = transform.GetChild(0).GetComponent<Animator>();
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
            isWalk = false;
            isSprint = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            canMove = false;
        }

        if(isWalk)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
        if(isSprint)
        {
            animator.SetBool("isSprint", true);
        }
        else
        {
            animator.SetBool("isSprint", false);
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
        if (horizontalMove > 0 && !facingRight)
        {
            TurnPlayer();
        }
        else if (horizontalMove < 0 && facingRight)
        {
            TurnPlayer();
        }
        isWalk = horizontalMove != 0 ? true : false;
        isSprint = Input.GetKey(KeyCode.LeftShift) ? true : false;
        PLAYER_SPEED = isSprint ? SPRINT_SPEED : WALK_SPEED;
        rb.velocity = new Vector2(horizontalMove * PLAYER_SPEED, rb.velocity.y);
    }

    void TurnPlayer()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
