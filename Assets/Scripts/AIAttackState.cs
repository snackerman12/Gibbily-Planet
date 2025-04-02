using UnityEngine;

public class AIAttackState : AIState
{
    public override void EnterState(AIController ai)
    {
        ai.SetSpeed(0);
        Attack(ai);
    }

    public override void UpdateState(AIController ai)
    {
        if (!ai.IsCloseToPlayer())
        {
            ai.stateMachine.ChangeState(new AIStalkingState(), ai);
        }
    }

    public override void ExitState(AIController ai) { }

    private void Attack(AIController ai)
{
    Debug.Log("AI Attacks Player!");

    if (ai.player != null)
    {
        PlayerHealth playerHealth = ai.player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Vector3 attackDirection = (ai.player.position - ai.transform.position).normalized;
            float attackForce = 10f; // Adjust as needed

            playerHealth.TakeDamage(100f, attackDirection * attackForce);
        }
    }
}


}

