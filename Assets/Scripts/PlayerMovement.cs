using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform graphics;
    [SerializeField] private Transform helpers;
    [SerializeField] private int maxSpeed = 5;
    [SerializeField] private int jumpForce;
    private Rigidbody2D rigidbody;

    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;

    private int extraJump;
    [SerializeField] private int extraJumpValue;


   
    void Start()
    {
        extraJump = extraJumpValue;
        rigidbody = GetComponent<Rigidbody2D>();              
    }

   
    void Update()   
    {
        Jump();
        if (isGrounded == true)
        {
            animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.x));
            animator.SetBool("Jump", false);
        }
        if (isGrounded == false)
        {
            animator.SetBool("Jump", true);
        }

        if (Mathf.Abs(rigidbody.velocity.x) < 0.01f)
        {
            return;
        }

        float xScale = rigidbody.velocity.x > 0 ? 1 : -1f;
        if (xScale < 0 && graphics.localScale.x < 0)
        {
            return;
        }

        if (xScale > 0 && graphics.localScale.x > 0)
        {
            return;
        }
        
        graphics.localScale = new Vector3(xScale, 1f, 1f);

        float xAngle = rigidbody.velocity.x > 0 ? 0f : 180f;
        helpers.localEulerAngles = new Vector3(0f, xAngle, 0f);        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.x = direction.x * maxSpeed;
        rigidbody.velocity = velocity;        
    }   

    private void Jump()
    {        
        if (isGrounded == true)
        {
            extraJump = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJump >0)
        {
            rigidbody.velocity = Vector2.up * jumpForce;
            extraJump--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJump == 0  && isGrounded == true)
        {
            rigidbody.velocity = Vector2.up * jumpForce;
        }
    }
}
