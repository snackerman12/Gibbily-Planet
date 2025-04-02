using UnityEngine;

public class DirectorAI : MonoBehaviour
{
    public AIController ai;

    private void Update()
    {
        AdjustAIState();
    }

    private void AdjustAIState()
    {
        if (ai.CanSeePlayer())
        {
            ai.fearLevel += Time.deltaTime * 0.5f;
        }
        else
        {
            ai.fearLevel -= Time.deltaTime * 0.3f;
        }

        ai.fearLevel = Mathf.Clamp(ai.fearLevel, 0, 10);

        if (ai.fearLevel >= 7)
        {
            ai.SetSpeed(ai.chaseSpeed);
        }
        else if (ai.fearLevel >= 4)
        {
            ai.SetSpeed(ai.stalkingSpeed);
        }
        else
        {
            ai.SetSpeed(ai.patrolSpeed);
        }
    }
}
