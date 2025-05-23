using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public ScoreManager scoreManager;

    void OnCollisionEnter(Collision collision)
    {
        scoreManager.ReduceDrivingScore(1000);
    }
}
