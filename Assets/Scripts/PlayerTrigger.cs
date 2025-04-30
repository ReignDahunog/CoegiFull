using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    public ScoreManager scoreManager;

    void OnCollisionEnter(Collision collision)
    {
        scoreManager.ReduceScore(1000);
    }
}
