using UnityEngine;

public class AIStealthDetection : MonoBehaviour
{
    public AIVision vision;
    public AIHearing hearing;
    public float detectionThreshold = 1f;
    
    // Returns true if detection level meets threshold or a sound was heard.
    public bool IsPlayerDetected()
    {
        bool visionDetected = vision != null && vision.detectionLevel >= detectionThreshold;
        bool hearingDetected = hearing != null && hearing.heardSound;
        return visionDetected || hearingDetected;
    }
}
