using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AIEnemy : MonoBehaviour
{
    public enum AIState
    {
        Roaming,
        Investigating,
        Chasing,
        Attacking,
        Recovering
    }

    public AIState currentState = AIState.Roaming;

    // Sensory Parameters
    public float hearingRange = 10f;    // Distance at which AI can hear sounds
    public float smellRange = 2f;       // Distance at which AI can smell the player
    public float investigateSpeed = 2f; // Speed at which AI investigates
    public float chaseSpeed = 8f;      // Speed at which AI chases the player
    public float attackRange = 3f;     // Range at which the AI will attack
    public float tooFarDistance = 15f; // Distance threshold where the AI stops chasing
    public float stopChaseCooldown = 1f; // Cooldown time (in seconds) before returning to roaming
    public float attackCooldown = 2f;   // Time between attacks
    public float attackDuration = 1f;   // Duration of the attack animation
    public float rotationSpeed = 5f;    // Speed at which the monster rotates to face player

    // References to player and other components
    public Transform player;           // Reference to the player
    private Vector3 lastHeardPosition;
    private NavMeshAgent navAgent;     // NavMeshAgent for movement
    private CharacterController playerController; // Reference to the player's CharacterController
    private Vector3 lastPlayerPosition; // Used for movement check
    private Animator animator;         // Reference to the monster's animator
    private float stopChaseTimer;      // Timer for waiting after player gets too far
    private float attackTimer;         // Timer for attack cooldown
    private float currentAttackTimer;  // Timer for current attack duration

    private Rigidbody _rb;

    // Initialize components
    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = chaseSpeed;
        playerController = player.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        lastPlayerPosition = player.position;
        SwitchState(AIState.Roaming);

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case AIState.Roaming:
                Roam();
                break;
            case AIState.Investigating:
                Investigate();
                break;
            case AIState.Chasing:
                Chase();
                break;
            case AIState.Attacking:
                Attack();
                break;
            case AIState.Recovering:
                Recover();
                break;
        }

        CheckForPlayerProximity();

        // Handle the cooldown timer for stopping the chase
        if (currentState == AIState.Chasing && stopChaseTimer > 0)
        {
            stopChaseTimer -= Time.deltaTime;
        }
        else if (stopChaseTimer <= 0 && currentState == AIState.Chasing)
        {
            SwitchState(AIState.Roaming);
        }

        // Handle attack cooldown
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void Roam()
    {
        if (!navAgent.hasPath || navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            Vector3 randomPosition = GetRandomNavMeshPosition();
            navAgent.SetDestination(randomPosition);

            animator.SetFloat("Walk", 1);
        }
    }

    private void Investigate()
    {
        if (lastHeardPosition != null)
        {
            navAgent.SetDestination(lastHeardPosition);
            animator.SetFloat("Walk", 1);
            if (Vector3.Distance(transform.position, lastHeardPosition) < 3f)
            {
                SwitchState(AIState.Roaming);
            }
        }
    }

    private void Chase()
    {
        navAgent.SetDestination(player.position);
        
        // Face the player while chasing
        FacePlayer();

        // If AI reaches the player and attack is not on cooldown, attack
        if (Vector3.Distance(transform.position, player.position) <= attackRange && attackTimer <= 0)
        {
            SwitchState(AIState.Attacking);
        }

        // If the player is too far away, stop chasing and wait before roaming
        if (Vector3.Distance(transform.position, player.position) > tooFarDistance && stopChaseTimer <= 0)
        {
            stopChaseTimer = stopChaseCooldown;
        }
    }

    private void Attack()
    {
        // Stop moving during attack
        navAgent.isStopped = true;
        
        // Face the player during attack
        FacePlayer();

        // Start attack animation
        animator.SetTrigger("Rage");

        // Track attack duration
        currentAttackTimer += Time.deltaTime;

        // If attack animation is complete
        if (currentAttackTimer >= attackDuration)
        {
            // Deal damage to player
            SceneManager.LoadScene(4);
            
            // Set attack cooldown and switch to recovery state
            attackTimer = attackCooldown;
            SwitchState(AIState.Recovering);
        }
    }

    private void Recover()
    {
        // Stay in recovery state until attack cooldown is over
        if (attackTimer <= 0)
        {
            navAgent.isStopped = false;
            SwitchState(AIState.Chasing);
        }
    }

    private void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep the monster upright
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void CheckForPlayerProximity()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= smellRange && currentState != AIState.Attacking && currentState != AIState.Recovering)
            {
                SwitchState(AIState.Chasing);
            }
            else if (distance <= hearingRange && IsPlayerMoving() && currentState != AIState.Chasing && currentState != AIState.Attacking && currentState != AIState.Recovering)
            {
                lastHeardPosition = player.position;
                SwitchState(AIState.Investigating);
            }
        }
    }

    private bool IsPlayerMoving()
    {
        Vector3 playerMovement = player.position - lastPlayerPosition;
        bool isMoving = playerMovement.magnitude > 0.1f;
        lastPlayerPosition = player.position;
        return isMoving;
    }

    private void SwitchState(AIState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            Debug.Log("AI is now in state: " + currentState);
            
            // Reset attack timer when entering attack state
            if (newState == AIState.Attacking)
            {
                currentAttackTimer = 0f;
            }
        }
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        randomPosition.y = 0f;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Fixes bug when monster hits something
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        if (collision.collider.CompareTag("person") && currentState != AIState.Attacking && currentState != AIState.Recovering)
        {
            SwitchState(AIState.Attacking);
        } 
    }
}
