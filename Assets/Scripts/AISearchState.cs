using UnityEngine;

public class AISearchState : AIState
{
    private float searchTimer = 5f;

    public override void EnterState(AIController ai)
    {
        ai.SetSpeed(ai.patrolSpeed);
        ai.MoveTo(ai.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
    }

    public override void UpdateState(AIController ai)
    {
        searchTimer -= Time.deltaTime;

        if (ai.CanSeePlayer())
        {
            ai.stateMachine.ChangeState(new AIChaseState(), ai);
        }
        else if (searchTimer <= 0)
        {
            ai.stateMachine.ChangeState(new AIPatrolState(), ai);
        }
    }

    public override void ExitState(AIController ai) { }
}
