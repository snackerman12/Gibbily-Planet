using UnityEngine;

public class AIVision : MonoBehaviour
{
    public Transform player;

    [Header("Vision Settings")]
    public float narrowRange = 20f; // long-range, narrow vision
    public float mediumRange = 15f; // medium-range vision
    public float wideRange = 10f;   // short-range peripheral vision

    public float narrowAngle = 5f;   // narrow cone total angle
    public float mediumAngle = 90f;  // medium cone total angle
    public float wideAngle = 180f;   // wide cone total angle

    public LayerMask obstructionMask;

    [Header("Detection Settings")]
    public float detectionLevel = 0f;
    public float detectionThreshold = 1f;
    public float detectionIncreaseRate = 0.8f;
    public float detectionDecreaseRate = 0.5f;

    private void Update()
    {
        if (player == null)
            return;

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        // Check line-of-sight using a raycast
        bool hasLOS = !Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstructionMask);

        if (hasLOS)
        {
            // Instant detection if player is in the narrow cone
            if (distanceToPlayer <= narrowRange && angle <= narrowAngle / 2f)
            {
                detectionLevel = detectionThreshold;
            }
            // Gradual detection if player is in the medium cone
            else if (distanceToPlayer <= mediumRange && angle <= mediumAngle / 2f)
            {
                detectionLevel = Mathf.MoveTowards(detectionLevel, detectionThreshold, detectionIncreaseRate * Time.deltaTime);
            }
            // Even slower buildup in the wide cone (peripheral vision)
            else if (distanceToPlayer <= wideRange && angle <= wideAngle / 2f)
            {
                detectionLevel = Mathf.MoveTowards(detectionLevel, detectionThreshold * 0.5f, detectionIncreaseRate * Time.deltaTime);
            }
            else
            {
                detectionLevel = Mathf.MoveTowards(detectionLevel, 0f, detectionDecreaseRate * Time.deltaTime);
            }
        }
        else
        {
            detectionLevel = Mathf.MoveTowards(detectionLevel, 0f, detectionDecreaseRate * Time.deltaTime);
        }
    }
}
