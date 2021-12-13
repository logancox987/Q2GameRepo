using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicMove : MonoBehaviour
{
    public float scaler;
    private Rigidbody2D rb2;
    public float angle;
    public float puntForce;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(Time.deltaTime);

        //Motion for character
        float inX =Time.deltaTime * scaler * Input.GetAxis("Horizontal");
        float inY = Time.deltaTime * scaler * Input.GetAxis("Vertical");
        rb2.AddForce(new Vector2(inX, inY));

        //Listen for a punt
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Punt");
            float fx = puntForce * Mathf.Cos(angle * Mathf.Deg2Rad);
            float fY = puntForce * Mathf.Sin(angle * Mathf.Deg2Rad);
            rb2.AddForce(new Vector2(fx, fY));
        }

        //Listen for a Reset (ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = new Vector3(0, 0, 0);
            rb2.velocity = new Vector2(0, 0);
        }
    }
}
