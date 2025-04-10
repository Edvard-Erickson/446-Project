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
        Attacking
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

    // References to player and other components
    public Transform player;           // Reference to the player
    private Vector3 lastHeardPosition;

    private NavMeshAgent navAgent;     // NavMeshAgent for movement
    private CharacterController playerController; // Reference to the player's CharacterController
    private Vector3 lastPlayerPosition; // Used for movement check

    private float stopChaseTimer; // Timer for waiting after player gets too far

    // Initialize NavMeshAgent and CharacterController
    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = chaseSpeed;  // Set default movement speed to chase speed
        playerController = player.GetComponent<CharacterController>(); // Assuming the player has a CharacterController
        lastPlayerPosition = player.position; // Initialize the last position
        SwitchState(AIState.Roaming); // Start by roaming
    }

    // Update method where we handle AI states
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
        }

        // Check for player proximity for chasing or attacking
        CheckForPlayerProximity();

        // Handle the cooldown timer for stopping the chase
        if (currentState == AIState.Chasing && stopChaseTimer > 0)
        {
            stopChaseTimer -= Time.deltaTime; // Countdown
        }
        else if (stopChaseTimer <= 0 && currentState == AIState.Chasing)
        {
            SwitchState(AIState.Roaming); // Switch back to roaming after cooldown
        }
    }

    // Roaming logic: Move to a random position
    private void Roam()
    {
        if (!navAgent.hasPath || navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            // Pick a random position on the NavMesh to roam to
            Vector3 randomPosition = GetRandomNavMeshPosition();
            navAgent.SetDestination(randomPosition);
        }
    }

    // Investigate logic: Move to the last heard position
    private void Investigate()
    {
        if (lastHeardPosition != null)
        {
            navAgent.SetDestination(lastHeardPosition);

            // If the AI has reached the heard position, go back to roaming
            if (Vector3.Distance(transform.position, lastHeardPosition) < 3f)
            {
                SwitchState(AIState.Roaming);
            }
        }
    }

    // Chase logic: Chase the player
    private void Chase()
    {
        navAgent.SetDestination(player.position);

        // If AI reaches the player, attack
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            SwitchState(AIState.Attacking);
            Attack();
        }

        // If the player is too far away, stop chasing and wait before roaming
        if (Vector3.Distance(transform.position, player.position) > tooFarDistance && stopChaseTimer <= 0)
        {
            Debug.Log("Player is too far. Stopping chase...");
            stopChaseTimer = stopChaseCooldown; // Start cooldown before roaming
        }
    }

    // Attack logic: Perform an attack when in chase state
    private void Attack()
    {
        Debug.Log("AI attacks the player!");

        SceneManager.LoadScene(4);

        // After attack, switch back to roaming
        SwitchState(AIState.Roaming);
    }

    // Check if the player is close to the AI
    private void CheckForPlayerProximity()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= smellRange) // If the player is very close, start chasing
            {
                if (currentState != AIState.Chasing)
                {
                    SwitchState(AIState.Chasing);
                }
            }
            else if (distance <= hearingRange && IsPlayerMoving()) // If player moves and is heard
            {
                lastHeardPosition = player.position;
                if (currentState != AIState.Investigating)
                {
                    SwitchState(AIState.Investigating);
                }
            }
        }
    }

    // Check if the player is moving (needed for hearing detection)
    private bool IsPlayerMoving()
    {
        // Using CharacterController movement to check if the player is moving
        Vector3 playerMovement = player.position - lastPlayerPosition;

        // If the player has moved significantly in this frame, they're considered moving
        bool isMoving = playerMovement.magnitude > 0.1f;

        // Update the last known player position
        lastPlayerPosition = player.position;

        return isMoving;
    }

    // Switch states and print when state changes
    private void SwitchState(AIState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            Debug.Log("AI is now in state: " + currentState);
        }
    }

    // Get a random position on the NavMesh
    private Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 10f;  // Random position within a sphere
        randomPosition.y = 0f; // Ensure the position is on the ground
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position; // Fallback to current position if no valid position found
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.collider.name);  // Debug log to confirm collision

        if (collision.collider.CompareTag("person"))
        {
            Attack();
        }
    }
}
