using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    [Range(2, 10)]
    public float smoothFactor;
    public float yLock;

    public void Start()
    {
        yLock = 0.1f;
    }

    public void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position =  new Vector3(targetPosition.x, yLock, targetPosition.z);
    }
}
