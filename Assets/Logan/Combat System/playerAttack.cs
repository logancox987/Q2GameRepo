using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public GameObject player;
    public float playerAttackCD;
    public float playerCurrentCD;
    public float playerSlamAttackCD;
    public float playerCurrentSlamCD;
    public bool IsGrounded;
    public bool slamAttackStarting;
    public Rigidbody2D rb2;
    public bool canAttackAfterSlam;

    public int slamAttackNumber = 1;

    public float camShakeAmt = 0.1f;
    public cameraShake camshake;
    
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;

    public Transform slamAttackPos;
    public float attackRangeX;
    public float attackRangeY;

    public Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        slamAttackStarting = false;
        canAttackAfterSlam = true;

    }

    // Update is called once per frame
    void Update()
    {

        if(slamAttackStarting == true)
        {
            playerCurrentSlamCD = playerSlamAttackCD;
        }
        else
        {
            playerCurrentSlamCD -= Time.deltaTime;
        }

        IsGrounded = player.GetComponent<playerMoveScript>().IsGrounded();

        if (playerCurrentCD <= 0)
        {
            if (Input.GetMouseButtonDown(0) && IsGrounded == true && canAttackAfterSlam == true)
            {
                playerCurrentCD = playerAttackCD;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponentInParent<enemyTakeDamage>().TakeDamage();
                    Debug.Log("Enemy Attacked");
                }
            }
        }
        else
        {
            playerCurrentCD -= Time.deltaTime;
        }

        if(playerCurrentSlamCD <= 0)
        {
            if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.S) && IsGrounded == false)
            {
                Debug.Log("Slam Attack Start");
                slamAttackStarting = true;
                slamAttackNumber = 1;
                canAttackAfterSlam = false;
            }
        }

        if(slamAttackStarting == true && IsGrounded == false)
        {
            player.GetComponent<playerMoveScript>().enabled = false;
            rb2.velocity = new Vector2(rb2.velocity.x * 0, rb2.velocity.y);
            player.GetComponent<playerMoveScript>().playerAnimator.SetBool("moving", false);
        }

        if(slamAttackStarting == true && IsGrounded == true)
        {

            slamAttackStarting = false;


            Invoke("enableMoveAfterSlam", 1.5f);
            Invoke("canAttackAfterSlamFunction", 1.5f);
            Invoke("shakeCam", 0.01f);


            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(slamAttackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                Debug.Log("HIT:               " + enemiesToDamage[i].gameObject.name);
                if (enemiesToDamage[i] != null)
                {
                    enemiesToDamage[i].gameObject.GetComponentInParent<enemyTakeDamage>().TakeSlamDamage();

                    Debug.Log("Enemy Slam Attacked");
                }
            }

        }


        if (slamAttackStarting == true && slamAttackNumber == 1)
        {
            playerAnimator.SetBool("slamAnim", true);
            Invoke("disableSlamAnim", .2f);
        }

        if(slamAttackNumber == 0)
        {
            playerAnimator.SetBool("slamAnim", false);
        }


    }

    public void disableSlamAnim()
    {
        slamAttackNumber = 0;
    }

    public void canAttackAfterSlamFunction()
    {
        canAttackAfterSlam = true;
    }

    public void shakeCam()
    {
        camshake.Shake(0.11f, 0.3f);
    }

    public void enableMoveAfterSlam()
    {
        player.GetComponent<playerMoveScript>().enabled = true;
        slamAttackStarting = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireCube(slamAttackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}