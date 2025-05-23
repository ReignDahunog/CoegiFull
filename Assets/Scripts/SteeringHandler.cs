using UnityEngine;

public class SteeringHandler : MonoBehaviour
{
    public ScoreManager scoreManager;

    void Update()
    {
        // Example placeholder for drift detection
        if (DetectDrift())
        {
            scoreManager.ReduceSteeringScore(1000);
        }
    }

    bool DetectDrift()
    {
        // Replace with actual drift detection logic
        return false;
    }
}
