
using UnityEngine;


public class AIStalkingState : AIState
{
    private bool isHiding;
    private Vector3 hidingSpot;
    private float stalkingTimer;
    private float stalkingDuration = 10f; // AI stalks for this long before deciding next action

    public override void EnterState(AIController ai)
    {
        ai.agent.speed = ai.stalkingSpeed; // Slow sneaky movement
        stalkingTimer = 0f;
        FindHidingSpot(ai);
    }

    public override void UpdateState(AIController ai)
    {
        stalkingTimer += Time.deltaTime;

        if (ai.CanSeePlayer()) 
        {
            if (Vector3.Distance(ai.transform.position, ai.player.position) <= ai.stalkingDistance)
            {
                ai.stateMachine.ChangeState(new AIChaseState(), ai); // Engage when close enough
                return;
            }
        }

        if (isHiding)
        {
            // AI moves to hiding spot and stops
            ai.agent.SetDestination(hidingSpot);

            if (Vector3.Distance(ai.transform.position, hidingSpot) < 1.5f)
            {
                ai.agent.isStopped = true; // AI stops moving when in position
            }
            else
            {
                ai.agent.isStopped = false; // Continue moving if not yet at hiding spot
            }
        }
        else
        {
            // AI sneaks around player without exposing itself
            Vector3 sneakPosition = ai.player.position + (Random.insideUnitSphere * 5f);
            sneakPosition.y = ai.transform.position.y;
            ai.agent.SetDestination(sneakPosition);
        }

        // If stalking for too long, randomly decide to attack or keep stalking
        if (stalkingTimer >= stalkingDuration)
        {
            if (Random.value > 0.4f) // 50% chance to chase
            {
                ai.stateMachine.ChangeState(new AIChaseState(), ai);
            }
            else
            {
                stalkingTimer = 0f; // Continue stalking but reset timer
                FindHidingSpot(ai);
            }
        }
    }

    public override void ExitState(AIController ai)
    {
        ai.agent.isStopped = false;
    }

    private void FindHidingSpot(AIController ai)
    {
        Collider[] objects = Physics.OverlapSphere(ai.transform.position, ai.visionRange, ai.obstacleMask);
    
        float bestCoverScore = 0f;
        Vector3 bestCoverPosition = ai.transform.position;

        foreach (Collider obj in objects)
        {
            if (obj.gameObject != ai.gameObject && obj.gameObject != ai.player.gameObject)
            {
                Vector3 coverPos = obj.transform.position;
                Vector3 dirToPlayer = (ai.player.position - coverPos).normalized;
                Vector3 hidePos = coverPos - (dirToPlayer * 2.0f); // Move behind the object

                // Check if AI is hidden
                if (!Physics.Linecast(hidePos, ai.player.position, ai.obstacleMask))
                {
                    continue; // Skip if it's not actually cover
                }

                float distanceToPlayer = Vector3.Distance(coverPos, ai.player.position);
                float distanceToAI = Vector3.Distance(ai.transform.position, coverPos);
                float coverScore = distanceToPlayer - distanceToAI;

                if (coverScore > bestCoverScore)
                {
                    bestCoverScore = coverScore;
                    bestCoverPosition = hidePos;
                }
            }
        }

        if (bestCoverScore > 0)
        {
            hidingSpot = bestCoverPosition;
            isHiding = true;
        }
        else
        {
            hidingSpot = ai.player.position + (Random.insideUnitSphere * 5f);
            hidingSpot.y = ai.transform.position.y;
            isHiding = false;
        }
    }
}
