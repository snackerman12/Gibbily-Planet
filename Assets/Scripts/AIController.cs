using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public AIStateMachine stateMachine;
    public float fearLevel = 0f; // Fear level for Director AI
    public LayerMask obstacleMask; // Used for detecting cover
    public float visionRange = 20f;
    public float lostPlayerTime = 5f;


    // Memory Variables
    private List<Vector3> pastPlayerPositions = new List<Vector3>();
    private Dictionary<Vector3, int> visitedLocations = new Dictionary<Vector3, int>();
    private Vector3 lastKnownPlayerPosition;
    private Vector3 predictedPlayerPosition;
    private float memoryDecayTime = 30f;
    private float timeSinceLastSeen = 0f;
    public enum AIState { Patrol, Chase, Attack, Search, Stalk }
    public AIState currentState;
    

    public float patrolSpeed = 5f;
    public float stalkingSpeed = 10f;
    public float stalkingDistance = 5f;
    public float chaseSpeed = 6f;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = new AIStateMachine();
        stateMachine.Initialize(new AIPatrolState(), this);
        
        

    }

    void Update()
    {
        stateMachine.Update(this);
        timeSinceLastSeen += Time.deltaTime;
        DecayMemory();

        
    }
    // Property for CurrentState
    public AIState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    // Method to get the target information (player name, etc.)
    public string GetTargetInfo()
    {
        return player ? player.name : "No Target";
    }

    public void UpdatePlayerPosition(Vector3 position)
    {
        lastKnownPlayerPosition = position;
        pastPlayerPositions.Add(position);

        if (pastPlayerPositions.Count > 5)
            pastPlayerPositions.RemoveAt(0);

        if (!visitedLocations.ContainsKey(position))
            visitedLocations[position] = 1;
        else
            visitedLocations[position]++;
    }

    public Vector3 PredictPlayerMovement()
    {
        if (pastPlayerPositions.Count < 2) return lastKnownPlayerPosition;

        Vector3 direction = pastPlayerPositions[pastPlayerPositions.Count - 1] - pastPlayerPositions[0];
        predictedPlayerPosition = lastKnownPlayerPosition + direction.normalized * 5f;
        return predictedPlayerPosition;
    }

    private void DecayMemory()
    {
        if (timeSinceLastSeen > memoryDecayTime)
        {
            pastPlayerPositions.Clear();
            visitedLocations.Clear();
            timeSinceLastSeen = 0f;
        }
    }
    public void SetSpeed(float speed)
    {
        if (agent != null)
            agent.speed = speed;
    }

    

    public void MoveTo(Vector3 position)
    {
        if (agent != null)
            agent.SetDestination(position);
    }

    public bool IsCloseToPlayer()
    {
        if (player == null) return false;
        return Vector3.Distance(transform.position, player.position) < 2f;
    }

     public bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        if (Vector3.Distance(transform.position, player.position) <= visionRange)
        {
            if (!Physics.Linecast(transform.position, player.position, obstacleMask))
            {
                lastKnownPlayerPosition = player.position;
                return true;
            }
        }
        return false;
    }

    public Transform GetPlayer()
    {
        return player;
    }

}


