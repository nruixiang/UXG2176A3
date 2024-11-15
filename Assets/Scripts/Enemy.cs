using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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
    [SerializeField] float minWalk;
    [SerializeField] float maxWalk;
    private float walkTimer;
    private Vector3 randomDirection;
    [SerializeField] float detectionRange;
    private bool hasLineOfSight;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        state = State.Patrol;
        enemyHealth = 10f;
        player = GameObject.FindGameObjectWithTag("Player");
        hasLineOfSight = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance();
        LineOfSightToPlayer();
        switch (state)
        {
            case State.Patrol:
            HandlePatrolState();
            break;
            case State.Chase:
            HandleChaseState();
            break;
            case State.Attack:
            break;
        }
        Debug.Log(state);
    }
    private void Patrol(){
        Debug.Log("Change Direction");
        float randomWalk = Random.Range(0f, 2f * Mathf.PI);

        randomDirection = new Vector3(Mathf.Sin(randomWalk), 0f, Mathf.Cos(randomWalk)).normalized;

        walkTimer = Random.Range(minWalk, maxWalk);
    }
    private void HandlePatrolState()
    {
        walkTimer -= Time.deltaTime;

        if (walkTimer <= 0f)
        {
            Patrol();
        }

        MoveEnemy(randomDirection, moveSpeed);
    }
    private void HandleChaseState()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        directionToPlayer.y = 0; //Keep movement on the ground plane

        MoveEnemy(directionToPlayer, moveSpeed);
    }
    private void MoveEnemy(Vector3 direction, float speed)
    {
        Vector3 movement = direction * speed;
        movement.y = Physics.gravity.y;
        
        characterController.Move(movement * Time.deltaTime);

        if(direction != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
    private void CheckPlayerDistance()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (state == State.Patrol && distanceToPlayer <= detectionRange)
        {
            if(hasLineOfSight){
                state = State.Chase;
            }
        } else if (state == State.Chase && distanceToPlayer > detectionRange){
        
            if(!hasLineOfSight){
                Debug.Log("Lost player! Returning to patrol state");
                state = State.Patrol;
                Patrol();
            }
        }
        
    }
    private void LineOfSightToPlayer(){
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - transform.position;

        if(Physics.Raycast(transform.position, directionToPlayer, out hit)){
            if (hit.collider.gameObject.tag == "Player"){
                Debug.Log("Player detected!");
                hasLineOfSight = true;

                } else{
                    hasLineOfSight = false;
                    Debug.Log("I CANT SEE you");

                }
        }
        Debug.DrawRay(transform.position, directionToPlayer, Color.red);
    }
}
