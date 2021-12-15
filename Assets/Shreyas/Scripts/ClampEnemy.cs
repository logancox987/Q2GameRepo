using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampEnemy : MonoBehaviour
{
    private Enemy_Behaviour enemyParent;
    public Transform leftBound;
    public Transform rightBound;
  

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(transform.position.x, leftBound.position.x, rightBound.position.x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

    }
}


