using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public LayerMask collisionMask;
    
    public Transform pointOne;
    public Transform pointTwo;

    public float moveTime = 1;
    private float moveSpeed;
    private Vector2 maximumDistance;

    private Rigidbody2D rb;
    private Transform moveToPoint;
    // Start is called before the first frame update
    void Start()
    {
        if (moveTime == 0.0f)
        {
            moveTime = 1;

        }
        moveToPoint = pointTwo;
        moveSpeed = Vector3.Distance(pointOne.position, pointTwo.position) / moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 distanceRemaining = moveToPoint.position - transform.position;
    }
}
