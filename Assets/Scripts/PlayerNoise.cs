using UnityEngine;

public class PlayerNoise : MonoBehaviour
{
    public AIController ai;

    public void MakeNoise()
    {
        if (ai != null)
        {
            ai.stateMachine.ChangeState(new AIStalkingState(), ai);
        }
    }
}
