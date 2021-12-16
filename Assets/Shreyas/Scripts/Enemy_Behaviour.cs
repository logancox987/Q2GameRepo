using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //Minimum distence for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public Transform leftBoundary;
    public Transform rightBoundary;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //Check if player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    public float flipWait = 2;
    public bool wait = true;
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance; //Store the distance between the enemy and player
    private bool attackMode;
    private bool cooldown; //Check if enemy is in cooldown after attack
    private float intTimer;
    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if(!attackMode && wait)
        {
            Move();
            

        }

        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            SelectTarget();
            
        }
        
        if(inRange)
        {            
            EnemyLogic();
        }
    }

    void EnemyLogic ()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance)
        {
            StopAttack();
        }   
        else if(attackDistance >= distance && cooldown == false)
        {
            Attack();
        }

        if(cooldown)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }

    }

    void Move()
    {
        
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }


    }



    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooldown && attackMode)
        {
            cooldown = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooldown = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooldown()
    {
        cooldown = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftBoundary.position.x && transform.position.x < rightBoundary.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftBoundary.position);
        float distanceToRight = Vector2.Distance(transform.position, rightBoundary.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftBoundary;
            
        }
        else
        {
            target = rightBoundary;
        }

        StartCoroutine(waitToFlip());
        

    }      


    IEnumerator waitToFlip()
    {
        anim.SetBool("canWalk", false);
        anim.Play("Enemy_idle");
        
        wait = false;
        
        yield return new WaitForSeconds(flipWait);
        
        Flip();
       
        wait = true;
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 0f;
           
        }
        else
        {
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;
    }
}
