using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovingWall : MonoBehaviour
{
    public float speed = 2f;
    public Transform endPos;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Save the starting position
    }

    void Update()
    {
        // Move the wall towards the target position along the X axis
        if (transform.position.x > endPos.position.x)
        {
            //Set the position towards the next step
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, endPos.position.x, speed * Time.deltaTime),
                transform.position.y,
                transform.position.z
            );
        }
    }
}
