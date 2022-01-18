using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpThroughPlatform : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(0f, .4f, 0f);
    public LayerMask player;
    private PlatformEffector2D platEffector;
    public float downTime;
    public GameObject bottomPlatCheck;

    void Start()
    {
        bottomPlatCheck = GameObject.Find("bottomPlatCheck");
        platEffector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && platGrounded() == true)
        {
            platEffector.rotationalOffset = 180f;
            Invoke("resetPlatform", 1f);
        }


        if (Input.GetButton("Jump") && platEffector.rotationalOffset == 180)
        {
            platEffector.rotationalOffset = 0f;
        }

    }


    public bool platGrounded()
    {
        bool platGrounded = Physics2D.BoxCast(transform.position + new Vector3(0f, 0f, 0f), boxSize, 0, Vector2.down, 0.7f, player);

        return platGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0f, 0f, 0f), boxSize);
    }

    private void resetPlatform()
    {
        platEffector.rotationalOffset = 0f;
    }


}
