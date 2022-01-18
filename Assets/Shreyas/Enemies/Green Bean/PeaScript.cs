using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaScript : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletrb2;

    // Start is called before the first frame update
    void Start()
    {

        bulletrb2 = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletrb2.velocity = new Vector2(moveDir.x, moveDir.y);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }







}
