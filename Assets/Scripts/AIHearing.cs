using UnityEngine;

public class AIHearing : MonoBehaviour
{
    [Header("Hearing Settings")]
    public float hearingRadius = 15f;
    public float soundAlertTime = 3f;
    private float alertTimer = 0f;
    public bool heardSound = false;

    private void Update()
    {
        if (heardSound)
        {
            alertTimer -= Time.deltaTime;
            if (alertTimer <= 0f)
            {
                heardSound = false;
            }
        }
    }

    // Call this method from any sound emitter (e.g., player footsteps)
    public void OnHearSound(Vector3 soundPosition, float soundVolume = 1f)
    {
        float effectiveHearingRadius = hearingRadius * soundVolume;
        if (Vector3.Distance(transform.position, soundPosition) <= effectiveHearingRadius)
        {
            heardSound = true;
            alertTimer = soundAlertTime;
            Debug.Log("AI heard a sound!");
        }
    }
}
