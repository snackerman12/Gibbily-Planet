public abstract class AIState
{
    public abstract void EnterState(AIController ai);
    public abstract void UpdateState(AIController ai);
    public abstract void ExitState(AIController ai);
}

