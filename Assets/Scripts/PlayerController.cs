using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public float jumpForce = 2.5f;
    public float longIdleTime = 5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    private Rigidbody2D _rigidbody;
    private Animator animator;

    private float longIdleTimer;

    private Vector2 movement;
    private bool facingRight = true;
    private bool isGrounded;

    private bool isAttacking;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking == false)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            movement = new Vector2(horizontalInput, 0f);

            if (horizontalInput < 0f && facingRight == true)
            {
                Flip();
            }
            else if (horizontalInput > 0f && facingRight == false)
            {
                Flip();
            }
        }
       

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(Input.GetButtonDown("Jump")&& isGrounded == true && isAttacking==false)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if(Input.GetButtonDown("Fire1")&& isGrounded==true && isAttacking == false)
        {
            movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
        }
    }
    void FixedUpdate()
    {
        if (isAttacking == false)
        {
            float horizontalVelocity = movement.normalized.x * speed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);

        }

    }
    void LateUpdate()
    {
        animator.SetBool("Idle", movement == Vector2.zero);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            longIdleTimer += Time.deltaTime;
            if (longIdleTimer >= longIdleTime)
            {
                animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            longIdleTimer = 0f;
        }
        

        }
    private void Flip()
    {
        facingRight = !facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale= new Vector3 (localScaleX,transform.localScale.y,transform.localScale.z);
    }
}
