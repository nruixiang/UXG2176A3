using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State{
        Patrol, Chase, Attack, Death
    }
    private State state;
    
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
    private bool canDamage;
    private GameObject player;
    private Player playerScript;
    private MeshRenderer enemyRenderer;
    private Color originalColor;
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        enemyRenderer = GetComponent<MeshRenderer>();
        originalColor = enemyRenderer.material.color;
        characterController = GetComponent<CharacterController>();
        state = State.Patrol;
        enemyHealth = 10f;
        player = GameObject.FindGameObjectWithTag("Player");
        hasLineOfSight = false;
        canDamage = true;
        
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
            case State.Death:
            EnemyDie();
            break;
        }
        //Checking if enemy is dead
        if(enemyHealth <= 0){
            state = State.Death;
        }
    }
    //Randomly set a direction to walk around
    private void Patrol(){
        float randomWalk = Random.Range(0f, 2f * Mathf.PI);

        randomDirection = new Vector3(Mathf.Sin(randomWalk), 0f, Mathf.Cos(randomWalk)).normalized;

        walkTimer = Random.Range(minWalk, maxWalk);
    }
    //Randomly walk around
    private void HandlePatrolState()
    {
        walkTimer -= Time.deltaTime;

        if (walkTimer <= 0f)
        {
            Patrol();
        }

        MoveEnemy(randomDirection, moveSpeed);
    }
    //Move the enemy towards the player
    private void HandleChaseState()
    {
        if (player == null) return;

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        directionToPlayer.y = 0; //Keep movement on the ground plane

        MoveEnemy(directionToPlayer, moveSpeed);
    }
    //Moves the enemy
    private void MoveEnemy(Vector3 direction, float speed)
    {
        Vector3 movement = direction * speed;
        movement.y = Physics.gravity.y;
        
        characterController.Move(movement * Time.deltaTime);

        if(direction != Vector3.zero){
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    //Checks distance between enemy & player to transition between states
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
                state = State.Patrol;
                Patrol();
            }
        }
        
    }
    //Raycast to detect player for Line of Sight
    private void LineOfSightToPlayer(){
        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - transform.position;

        if(Physics.Raycast(transform.position, directionToPlayer, out hit, layerMask)){
            if (hit.collider.gameObject.tag == "Player"){
                hasLineOfSight = true;

                } else{
                    hasLineOfSight = false;

                }
        }
    }
    //Damage the player
    private void OnControllerColliderHit(ControllerColliderHit col){
        if(col.collider.CompareTag("Player")){
            if(canDamage){
                playerScript = col.collider.GetComponent<Player>();
                StartCoroutine(EnemyDoDamage());
            }
            
        }
    }
    //Damage delay
    private IEnumerator EnemyDoDamage(){
        canDamage = false;
        playerScript.TakeDamage();
        yield return new WaitForSeconds(1);
        canDamage = true;
    }
    //Feedback for when enemy takes damage
    public IEnumerator DamageFeedback(){
        enemyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        if(enemyRenderer != null){
            enemyRenderer.material.color = originalColor;
        }
        
    }
    //Destroy enemy
    private void EnemyDie(){
        UiManager.progress += 1;
        Destroy(gameObject);
    }
}
