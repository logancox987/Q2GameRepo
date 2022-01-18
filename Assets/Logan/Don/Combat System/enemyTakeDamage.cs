using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTakeDamage : MonoBehaviour
{
    public int startHealth;
    public int health;

    public Rigidbody2D rb2;
    private Animator anim;

    public GameObject player;
    public GameObject enemy;
    public GameObject hitBox;
    public float waitForDeath;




    playerAttack playerAttack;
    playerMoveScript playermove;

    [Header ("Attack Settings")]
    public float knockbackForce;
    public float knockbackWait;

    [Header("Slam Settings")]
    public float slamKnockbackForcex;
    public float slamKnockbackForcey;
    public float slamKnockbackWait;
    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<playerAttack>();
        playermove = GetComponent<playerMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 1)
        {
            rb2.velocity = new Vector2(0, 0);
            enemy.GetComponent<Enemy_Behaviour>().enabled = false;
            hitBox.SetActive(false);
            StartCoroutine(WaitForDeath());
        }
    }

    public void TakeDamage()
    {
        health = health - 1;
        StartCoroutine(Knockback());
        
    }
    
    
    public void TakeSlamDamage()
    {
        health = health - 5;
        StartCoroutine(slamKnockback());

    }

    IEnumerator Knockback()
    {
        if (player.transform.position.x < enemy.transform.position.x)
        {
            rb2.velocity = new Vector2(knockbackForce, 0);
            hitBox.SetActive(false);
            anim.SetBool("Knockback", true);

        }
        if (player.transform.position.x > enemy.transform.position.x)
        {
            rb2.velocity = new Vector2(-knockbackForce, 0);
            hitBox.SetActive(false);
            anim.SetBool("Knockback", true);
        }


        yield return new WaitForSeconds(knockbackWait);

        
        anim.SetBool("Knockback", false);
        hitBox.SetActive(true);
        rb2.velocity = new Vector2(rb2.velocity.x * 0, rb2.velocity.y);
    }
    IEnumerator slamKnockback()
    {
        if (player.transform.position.x < enemy.transform.position.x)
        {
            rb2.velocity = new Vector2(slamKnockbackForcex, slamKnockbackForcey);
            hitBox.SetActive(false);
            anim.SetBool("Knockback", true);
        }
        if (player.transform.position.x > enemy.transform.position.x)
        {
            rb2.velocity = new Vector2(-slamKnockbackForcex, slamKnockbackForcey);
            hitBox.SetActive(true);
            anim.SetBool("Knockback", true);
        }


        yield return new WaitForSeconds(slamKnockbackWait);

        anim.SetBool("Knockback", false);
        hitBox.SetActive(true);
        rb2.velocity = new Vector2(rb2.velocity.x * 0, rb2.velocity.y);
    }

    IEnumerator WaitForDeath()
    {
        anim.SetBool("Knockback", false);
        anim.Play("Death");

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
