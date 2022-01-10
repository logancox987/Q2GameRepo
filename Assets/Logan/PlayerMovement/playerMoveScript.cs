using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveScript : MonoBehaviour

{
    public float offset;
    public Vector3 boxSize = new Vector3(3f, .4f, 0f) - new Vector3(0,1,0);
    float coyoteRemember = 0;
    [SerializeField]
    float coyoteTime = 0.25f;
    float jumpStorage = 0;
    [SerializeField]
    float jumpStorageTime = 0.25f;
    [SerializeField]
    private float jumpCut = 0.5f;
    public LayerMask ground;
    [SerializeField]
    private float jumpPower = 1f;
    private float horizontal;
    [SerializeField]
    private float moveSpeed = 1f;
    public Rigidbody2D rb2;

    private bool facingRight = true;

    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        GameManager.enableMove = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (rb2.velocity.x != 0)
        {
            playerAnimator.SetBool("moving", true);
        }
        else
        {
            playerAnimator.SetBool("moving", false);
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded() == true)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, jumpPower);
        }

        if(Input.GetButtonUp("Jump"))
        {
            if(rb2.velocity.y > 0)
            {
                rb2.velocity = new Vector2(rb2.velocity.x, rb2.velocity.y * jumpCut);
            }    
        }

        Flip();

        coyoteRemember -= Time.deltaTime;
        {
            if(IsGrounded())
            {
                coyoteRemember = coyoteTime;
            }
            
            jumpStorage -= Time.deltaTime;
            
            if(Input.GetButtonDown("Jump"))
            {
                jumpStorage = jumpStorageTime;
            }

            if((jumpStorage > 0) && (coyoteRemember > 0))
            {
                jumpStorage = 0;
                coyoteRemember = 0;
                rb2.velocity = new Vector2(rb2.velocity.x, jumpPower);
            }

        }
    }

    private void Flip()
    {
        if(facingRight && horizontal < 0f || !facingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            facingRight = !facingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.enableMove == true)
        {
            rb2.velocity = new Vector2(horizontal * moveSpeed, rb2.velocity.y);
        }
    }

    public bool IsGrounded()
    {
        bool grounded = Physics2D.BoxCast(transform.position + new Vector3(0f, -.865f, 0f), boxSize, 0, Vector2.down, 0.25f, ground);

        return grounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(.02f, -.865f, 0f), boxSize);
    }


}
