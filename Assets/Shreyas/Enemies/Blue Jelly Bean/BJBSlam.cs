using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BJBSlam : MonoBehaviour
{
    [Header("Slam")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Vector3[] directions = new Vector3[1];
    public float waitTime;

    [Header("Reset")]
    public Vector2 initial = new Vector2(0, 0);
    public Vector2 end = new Vector2(0, 0);
    public float stopTime;

    bool waitingToReset = false;
    bool reseting = false;
    private Animator BJBAnimations;
    private void OnEnable()
    {
        Stop();
    }
    private void Start()
    {
        BJBAnimations = GetComponent<Animator>();
    }
    void Update()
    {
        if(attacking)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), end, speed * Time.deltaTime);
            BJBAnimations.Play("BJ Jump");
            if(Vector2.Distance(transform.position, end) < 0.01f)
            {
                Stop();
            }
        }
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay)
            {
                CheckForPlayer();
                
            }
            if(checkTimer < checkDelay && !waitingToReset)
            {
                StartCoroutine(WaitToReset());
                BJBAnimations.Play("BJ Walk");
            }
            
        }

        if (reseting)
        {

            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), initial, 10 * Time.deltaTime);
            BJBAnimations.Play("BJ Walk");
            if(Vector2.Distance(transform.position, initial) < 0.01f)
            {
                reseting = false;
            }
        }
    }

    private void CheckForPlayer()
    {
        StartCoroutine(WaitToSlam());

    }
    private void Direction()
    {
        directions[0] = -transform.up * range;
    }
    private void Stop()
    {
        destination = transform.position;
        attacking = false;

    }

    private IEnumerator WaitToReset()
    {
        waitingToReset = true;
        yield return new WaitForSeconds(stopTime);
        reseting = true;
        waitingToReset = false;

    }
    private IEnumerator WaitToSlam()
    {
        Direction();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                BJBAnimations.Play("BJ WaitToSlam");
                yield return new WaitForSeconds(waitTime);
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }


        }


    }
}
