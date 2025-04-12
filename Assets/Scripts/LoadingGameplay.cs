using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGameplay : MonoBehaviour
{
    public Transform startPoint;  // Assign the starting position in Unity Inspector
    public Transform endPoint;    // Assign the ending position in Unity Inspector
    public float duration = 5f;   // Duration of animation

    private float elapsedTime = 0f; // Start from 0

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, progress);
        }
        else
        {
            // Load GamePlay scene (scene index 6) after animation completes
            SceneManager.LoadScene("EasyGameplay");
        }
    }
}
