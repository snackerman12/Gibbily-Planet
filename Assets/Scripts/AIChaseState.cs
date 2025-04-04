using UnityEngine;

public class AIChaseState : AIState
{
    private float lostPlayerTimer = 0f;

     public override void EnterState(AIController ai)
    {
        ai.agent.speed = ai.chaseSpeed;
        
    }

     public override void UpdateState(AIController ai)
    {
        if (!ai.CanSeePlayer())
        {
            lostPlayerTimer += Time.deltaTime;
            if (lostPlayerTimer >= ai.lostPlayerTime)
            {
                ai.stateMachine.ChangeState(new AISearchState(), ai);
            }
            else
            {
                PredictPlayerMovement(ai);
            }
        }
        else
        {
            lostPlayerTimer = 0f;
            ai.agent.SetDestination(ai.player.position);
        }
    }

    public override void ExitState(AIController ai) { }

    private void PredictPlayerMovement(AIController ai)
    {
        Vector3 predictedPosition = ai.player.position + (ai.player.forward * 3f);
        ai.agent.SetDestination(predictedPosition);
    }
}

