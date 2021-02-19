using System.Collections;
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
   


    public float jumpValue = 0.0f;
    public bool canJump = true;

    public Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        if(moveInput == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }

        if (moveInput > 0)
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
      
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);



        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            
        }

        
        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            
            jumpValue += 0.1f;
        }

        if (jumpValue >= 15f && isGrounded)
        {
            float tempx = moveInput * speed;
            float tempy = jumpValue;
            rb.velocity = new Vector2(tempx, tempy);
            Invoke("ResetJump", 0.2f);
        }

        if (Input.GetKeyUp(KeyCode.Space))

            if (isGrounded)
            {
                rb.velocity = new Vector2(moveInput * speed, jumpValue);
                jumpValue = 0.0f;
                
                
            }
            
        {
            canJump = true;
        }
        
        
      }

    private void ResetJump()
    {
        canJump = false;
        jumpValue = 0;
    }

}
 

