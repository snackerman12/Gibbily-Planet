public class AIStateMachine
{
    public AIState currentState;

    public void Initialize(AIState startingState, AIController ai)
    {
        currentState = startingState;
        currentState.EnterState(ai);
    }

    public void ChangeState(AIState newState, AIController ai)
    {
        currentState.ExitState(ai);
        currentState = newState;
        currentState.EnterState(ai);
    }

    public void Update(AIController ai)
    {
        currentState.UpdateState(ai);
    }
}


