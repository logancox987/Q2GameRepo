using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBeanScript : MonoBehaviour
{
    Vector3 pointA;
    Vector3 pointB;
    public float speed;

    public float lineOfSight;
    public float rotationSpeed;
    public Transform FirePoint;
    

    public Transform target;
    public GameObject projectile;
    private float lastAttackTime;
    public float attackDelay;
    public float force;

    public int numberOfBullets = 3;
    public float burstFireRate = .2f;

    void Start()
    {
        pointA = transform.eulerAngles + new Vector3(0f, 0f, 0f);
        pointB = transform.eulerAngles + new Vector3(0f, 0f, 0f);
    }

    void Update()
    {


        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        
        if(distanceToPlayer > lineOfSight)
        {
            float time = Mathf.PingPong(Time.time * speed, 1);
            transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
        }

        if (target.position.y > transform.position.y)
        {
            float time = Mathf.PingPong(Time.time * speed, 1);
            transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
        }



        if (distanceToPlayer < lineOfSight && target.position.y < transform.position.y)
        {
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg + 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);



            if (Time.time > lastAttackTime + attackDelay)
            {
                RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, FirePoint.TransformDirection(Vector2.down), lineOfSight);

              if (hit.transform == target)
              {
                    StartCoroutine(burst());
                    

              }
            }
        }


    }

    IEnumerator burst()
    {
        for(int i = 0; i < numberOfBullets; i++)
        {
            Fire();
            yield return new WaitForSeconds(burstFireRate);
        }
    }


    void Fire()
    {
        GameObject newPea = Instantiate(projectile, FirePoint.position, FirePoint.rotation);
        newPea.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, -force));
        lastAttackTime = Time.time;

    }

   

}











