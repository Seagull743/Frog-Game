using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    private float jumptimeCounter;
    public float jumptime;
    private bool isjumping;

    public float jumpBufferLength;
    private float jumpBufferCount;

    public SpriteRenderer sr;

    private Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(moveInput > 0)
        {
            sr.flipX = false;
        }
        else if(moveInput < 0)
        {
            sr.flipX = true;
        }
    }



    void Update()
    {



        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isjumping = true;
            jumptimeCounter = jumptime;
            rb.velocity = Vector2.up * jumpForce;
        }


        if (Input.GetKey(KeyCode.Space) && isjumping == true)
        {

            if (jumptimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumptimeCounter -= Time.deltaTime;
            }
            else
            {
                isjumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isjumping = false;
        }
    }
        
}
