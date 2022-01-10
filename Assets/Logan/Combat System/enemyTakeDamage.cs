using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTakeDamage : MonoBehaviour
{
    public int startHealth;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health < 1)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        health = health - 1;
    }
    
    public void TakeSlamDamage()
    {
        health = health - 5;
    }
}
