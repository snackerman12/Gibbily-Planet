using UnityEngine;
using UnityEngine.AI;

public class AIPatrolState : AIState
{
    private float patrolRadius = 20f;
    private float waitTime = 0.5f;
    private float waitCounter;

    public override void EnterState(AIController ai)
    {
        ai.agent.speed = ai.stalkingSpeed;
        MoveToRandomPoint(ai);
    }

    public override void UpdateState(AIController ai)
    {
        if (ai.CanSeePlayer())
        {
            ai.stateMachine.ChangeState(new AIStalkingState(), ai);
            return;
        }

        if (!ai.agent.pathPending && ai.agent.remainingDistance < 0.5f)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                MoveToRandomPoint(ai);
                waitCounter = 0f;
            }
        }
    }

    public override void ExitState(AIController ai)
    {
       
    }

    private void MoveToRandomPoint(AIController ai)
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += ai.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            ai.agent.SetDestination(hit.position);
        }
    }
}
