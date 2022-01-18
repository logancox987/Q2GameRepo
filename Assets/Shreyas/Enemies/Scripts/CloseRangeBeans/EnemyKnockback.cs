using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] float knockbackLength = 0.5f;
    [SerializeField] float knockbackForce = 15f;

    Enemy_Behaviour enemyScript;
    Rigidbody2D rb2;
    public GameObject player;
    public GameObject enemy;
    public float knockbackTime;

    public bool IsHurt
    {
        get { return isHurt; }
    }

    bool isHurt = false;
    void Start()
    {
        enemyScript = GetComponent<Enemy_Behaviour>();
        rb2 = GetComponent<Rigidbody2D>();
    }
    public void DoEnemyKnockback()
    {
        rb2.AddForce(transform.right * knockbackForce);

    }
}
