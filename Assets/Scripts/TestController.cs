﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private SpriteRenderer sr;

   
    public ParticleSystem Burst;

    public float jumpValue = 0.0f;
    public bool canJump = true;

    public Animator anim;

    public int timer;


  
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
            
        if(moveInput == 0 && isGrounded == true )
        {
            anim.SetBool("isWalking", false);            
        }

        else
        {
            anim.SetBool("isWalking", true);
        }

        if(!isGrounded)
        {
            anim.SetBool("isWalking", false);       
        }
       
       
        if (moveInput > 0 )
        {
            sr.flipX = false;
                          
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;
                     
        }

        moveInput = Input.GetAxisRaw("Horizontal");
       
        if(jumpValue == 0.0f && isGrounded)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
      



        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            anim.SetBool("TakeOff", true);          
        }


        
        if(isGrounded == true)
        {
            timer = 0;
            anim.SetBool("isJumping", false);           
        }
        else 
        {
            timer++;  
            anim.SetBool("isJumping", true);

            if(timer == 1f && jumpValue > 0.1f)
            {        
                Instantiate(Burst, feetPos.transform.position, Quaternion.identity);
                jumpValue = 0;
            }

            
        }


        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {      
            jumpValue += 0.5f;      
        }
              
        if (jumpValue >= 15f && isGrounded)
        {
            
            float tempx = moveInput * speed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded) 
        {    
            rb.velocity = new Vector2(moveInput * speed, jumpValue);         
        }
        
        canJump = true;

    }

   
       


    private void ResetJump()
    {
        canJump = false;
        jumpValue = 0;
    }

}
 

