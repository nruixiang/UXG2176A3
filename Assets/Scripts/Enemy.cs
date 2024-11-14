using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State{
        Patrol, Chase, Attack
    }
    private State state;
    //Enemy
    private CharacterController characterController;
    public float enemyHealth;
    [SerializeField] float moveSpeed = 1f;
    private float rotationSpeed = 120;
    //For Patrolling enemy
    [SerializeField] float minWalk;
    [SerializeField] float maxWalk;
    private float walkTimer;
    private Vector3 randomDirection;
    //
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        state = State.Patrol;
        enemyHealth = 10f;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Patrol:
            ChasePlayer();
            //HandlePatrolState();
            break;
            case State.Chase:
            break;
            case State.Attack:
            break;
        }
    }
    private void Patrol(){
        Debug.Log("Change Direction");
        float randomWalk = Random.Range(0f, 2f * Mathf.PI);

        randomDirection = new Vector3(Mathf.Sin(randomWalk), 0f, Mathf.Cos(randomWalk)).normalized;

        walkTimer = Random.Range(minWalk, maxWalk);
    }
    private void HandlePatrolState()
    {
        // Update timer
        walkTimer -= Time.deltaTime;

        // Choose new direction if timer expires or we hit something
        if (walkTimer <= 0f)
        {
            Patrol();
        }

        // Calculate movement with gravity
        Vector3 movement = randomDirection * moveSpeed;
        movement.y = Physics.gravity.y;
        
        // Move the enemy
        characterController.Move(movement * Time.deltaTime);

        // Rotate towards movement direction
        if (randomDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(randomDirection.x, 0, randomDirection.z));
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
    private void ChasePlayer(){
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

}
