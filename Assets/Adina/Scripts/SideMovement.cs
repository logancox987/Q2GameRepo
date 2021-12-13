using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovement : MonoBehaviour
{
    public float accel = 8;
    private Rigidbody2D rb2;
    private SpriteRenderer sr;
    public Animator animator;
    float horizontalMove = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        
        //Move right
        if(Input.GetAxis("Horizontal")> 0)
        {
            sr.flipX = false;
            rb2.AddForce(new Vector2 (accel, 0));
            animator.SetFloat("Speed", accel);
        }

        //Move left
        if (Input.GetAxis("Horizontal")< 0)
        {
            sr.flipX = true;
            rb2.AddForce(new Vector2(-accel, 0));
            animator.SetFloat("Speed", accel);      
        }

        //Not Moving
        if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetFloat("Speed", 0);
        }
    }
}
