
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Player controllers")]
    public CharacterController2D character;
    public Rigidbody2D rb;
    [Tooltip("Normal gravity of the player")]
    public float firstGravity;

    [Header("Speeds")]
    [Tooltip("Player's speed")]
    public float speed = 25f;
    [Tooltip("Player's speed when is climbing the ladder")]
    public float speedOnLadder = 5f;
    [Tooltip("The movement of the player. Input * speed")]
    public float movement;

    [Header("Player boolean actions")]
    [Tooltip("Is the player jumping?")]
    public bool jump;
    [Tooltip("Is the player climbing up?")]
    public bool climbingUp;

    [Header("Ladder Raycast")]
    public float distance;
    public LayerMask whatIsLadder;

    void Start()
    {
        //At the start we save the first value for gravity and dont change it wrongly in the future.
        firstGravity = rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        movement = Input.GetAxisRaw("Horizontal") * speed;

        //Making player jump when isn't on a Ladder
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        Debug.Log(rb.gravityScale);
    }

    void FixedUpdate()
    {
        //Movement
        Movement();

        //Climb ladders
        Climbing();

    }

    public void Movement()
    {
        character.Move(movement * Time.fixedDeltaTime, false, jump);
        Debug.Log(rb.velocity);
        jump = false;

    }

    public void Climbing()
    {
         bool climbing; //Is the player climbing?

        RaycastHit2D hitLadder = Physics2D.Raycast(transform.position, Vector2.down, distance, whatIsLadder); //What is +

        //Collides with a ladder
        if (hitLadder.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.green);
            Debug.Log("I'm on the ladder");
            if (Input.GetAxisRaw("Vertical") > 0) //If the user want to go up.
            {
                climbingUp = true;
                climbing = true;
            }
            else
            {
                climbing = false;
                climbingUp = false;
            } 

        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);
            Debug.Log("I'm not on ladder");
            //Reset the climbing booleans when hit the top of the ladder or when the player isn't in the ladder.
            climbing = false; 
            climbingUp = false;
        }

        //The velocity on X and Y when the player ir climbing.
        if (climbingUp == true)
        {
            rb.velocity = new Vector2(rb.velocity.x / 2, speedOnLadder);

            //If the player "jumps" when its climbing the player will stay in the air.
            if (Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x / 2, 0.981f);
            }
            
        }

    }

    /*public void OnTriggerStay2D(Collider2D other)
    {
        //Walking on Stairs ladder
        if (other.tag.Equals("Ladder"))
        {
            onLadder = true; //Player is on the Ladder
            speedOnLadder = rb.velocity.x / 2;
            if (Input.GetKey(KeyCode.W))
            {
                //Sube a 3f la escalera y sale a su velocidad normal
                rb.velocity = new Vector2(0f, 3f); //El movimiento del personaje , velocidad de subida de escaleras
                onLadder = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector2(0f, -3f);
                onLadder = true;
            }else
            {
                //Si ha subido y se mantiene, se realentiza su velocidad.
                rb.velocity = new Vector2(rb.velocity.x, 0.981f); //0.981f works for me. The player stays on the same position.
                
            }
        }
        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Stair"))
        {
            onLadder = false;
        }
    }*/
}