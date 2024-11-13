using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovingSide : MonoBehaviour
{
    public float speed = 2f;
    public float range = 5f; // Total range of movement (distance)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new position along the X-axis
        float newZ = startPosition.y + Mathf.PingPong(Time.time * speed, range);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
    }
}
