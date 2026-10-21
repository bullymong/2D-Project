using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float defaultSpeed;
    public float sprintSpeed;
    public float maxSpeed;
    public float horizontal;
    public float jumpForce;
    public Animator anim;

    [Header("Slide Settings")]
    public CapsuleCollider2D playerCollider;
    public float slideHeight;
    public float slideWidth;
    public Vector2 slideOffset;
    public float normalHeight;
    public float normalWidth;
    public Vector2 normalOffset;

    [Header("Crouch Check")]
    public Transform headCheck;
    public float headCheckRadius = .2f;

    [Header("Wall Check")]
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask wallLayer;

    [Header("Wall Movement")]
    public float wallSlideSpeed = 2;
    bool isWallSliding;
    bool isWallJumping;
    float wallJumpDirection;
    public float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);

    [Header ("Grounding")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public Transform platformCheck;
    public float platformCheckRadius;
    public LayerMask platformLayer;

    [Header("Hold to Jump Settings")]
    public float maxJumpTime = .3f;
    public float holdForce = 3;
    private bool isJumping;
    private float jumpTimeCounter;

    private int facingDirection = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        ProcessWallSlide();
        ProcessWallJump();
        //Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        if(horizontal > .1f && facingDirection < 0 || horizontal < -.1f && facingDirection > 0)
        {
            Flip();
        }
        //Sliding
        if (Input.GetKey("left shift"))
        {
            Debug.Log("Transition to slide");
            anim.SetTrigger("slide");
            if (speed < maxSpeed)
            {
                speed = speed + sprintSpeed;
                SetColliderSlide();
            }
           
        }
        else
        {
            Debug.Log("Transition to idle");
            speed = defaultSpeed;
            anim.SetTrigger("walk");

            TryStandUp();
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            anim.SetTrigger("jump");
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        //Continue Jumping
        if(Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, holdForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        //Stop Jumping
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;

        }

        //Wall Jumping
        if(Input.GetButtonDown("Jump") && wallJumpTimer > 0)
        {
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;
            if(transform.localScale.x != wallJumpDirection)
            {
                facingDirection *= -1;

                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
        }
    }

    void FixedUpdate()
    {
        //Movement
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    void TryStandUp()
    {
        bool canStandUp = !Physics2D.OverlapCircle(headCheck.position, headCheckRadius, platformLayer);
        if(canStandUp)
        {
            SetColliderNormal();
        }
        else
        {
            SetColliderSlide();
        }
    }

    void SetColliderNormal()
    {
        playerCollider.size = new Vector2(normalWidth, normalHeight);
        playerCollider.offset = normalOffset;
    }

    void SetColliderSlide()
    {
        playerCollider.size = new Vector2(slideWidth, slideHeight);
        playerCollider.offset = slideOffset;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) || Physics2D.OverlapCircle(platformCheck.position, platformCheckRadius, platformLayer);

    }

    void Flip()
    {
        facingDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool WallCheck()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }

    private void ProcessWallSlide()
    {
        if (!IsGrounded() && WallCheck() && horizontal != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void ProcessWallJump()
    {
        if(isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }
}
