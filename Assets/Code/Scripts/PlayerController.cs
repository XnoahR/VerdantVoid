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
    private bool canMove;
    public bool facingRight = true;

    [Header("Player Animation")]
    public Animator animator;
    public bool isWalk;
    public RuntimeAnimatorController[] animatorControllers;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip runSFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PLAYER_SPEED = WALK_SPEED;
    }

    void EnableControl()
    {
        canMove = true;
    }

    void DisableControl()
    {
        canMove = false;
    }

    public void setStateCutscene()
    {
        GameplayMaster.currentGameState = GameplayMaster.GameState.Cutscene;
    }

    void Update()
    {
        if (GameplayMaster.currentGameState == GameplayMaster.GameState.Gameplay)
        {
            canMove = LoadingScreenManager.isLoading ? false : true;
        }
        else
        {
            isWalk = false;
            isSprint = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            canMove = false;
        }

        AnimatorCheck();
        
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            GameplayMaster.currentChapter = 0;
            Debug.Log(GameplayMaster.currentChapter);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            GameplayMaster.currentChapter = 1;
            Debug.Log(GameplayMaster.currentChapter);
        }
        
        AnimationCheck();
    }

    private void AnimatorCheck(){
        int currentChapter = GameplayMaster.currentChapter;
        int currentStage = GameplayMaster.currentStage;
        animator.runtimeAnimatorController = currentChapter == 1 && currentStage == 1 ? animatorControllers[0] : animatorControllers[1];
    }

    private void AnimationCheck()
    {
        if (isWalk)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
        if (isSprint)
        {
            animator.SetBool("isSprint", true);
            if (!audioSource.isPlaying && isWalk)
            {
                audioSource.clip = runSFX;
                audioSource.Play();
            }
            //if not wakling, stop the sound
            if (!isWalk)
            {
                audioSource.Stop();
            }
        }
        else
        {
            animator.SetBool("isSprint", false);
            if (audioSource.isPlaying && audioSource.clip == runSFX || !isWalk)
            {
                audioSource.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            PlayerMovement();
        }
    }

    public void PlayerMovement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        if (horizontalMove > 0 && !facingRight)
        {
            TurnPlayer();
        }
        else if (horizontalMove < 0 && facingRight)
        {
            TurnPlayer();
        }
        isWalk = horizontalMove != 0;
        isSprint = Input.GetKey(KeyCode.LeftShift);
        PLAYER_SPEED = isSprint ? SPRINT_SPEED : WALK_SPEED;
        rb.velocity = new Vector2(horizontalMove * PLAYER_SPEED, rb.velocity.y);
    }

    public void TurnPlayer()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
