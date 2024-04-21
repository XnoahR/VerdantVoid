using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header ("Player Movement")]
    private Rigidbody2D rb;
    private float PLAYER_SPEED;
    [SerializeField] float WALK_SPEED = 2;
    [SerializeField] float SPRINT_SPEED = 5;
    private bool isSprint;


    [Header ("Player Interact")]
    [SerializeField] float interactRange = 1;

    // Start is called before the first frame update
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        PLAYER_SPEED = WALK_SPEED;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        CheckSprint();
    }

    void PlayerMovement(){
        float horizontalMove = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalMove * PLAYER_SPEED, rb.velocity.y);

        
    }

    void CheckSprint(){
        isSprint = Input.GetKey(KeyCode.LeftShift)? true : false;
        //2x press to sprint
        
        PLAYER_SPEED = isSprint ? SPRINT_SPEED : WALK_SPEED;
    }
    
}
