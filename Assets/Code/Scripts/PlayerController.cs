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

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        PLAYER_SPEED = WALK_SPEED;
    }

    void Update()
    {
        CheckSprint();
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMove * PLAYER_SPEED, rb.velocity.y);
    }

    void CheckSprint()
    {
        isSprint = Input.GetKey(KeyCode.LeftShift) ? true : false;
        //2x press to sprint

        PLAYER_SPEED = isSprint ? SPRINT_SPEED : WALK_SPEED;
    }
}
