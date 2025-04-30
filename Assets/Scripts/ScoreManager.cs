using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int finalScore = 10000;

    void Start()
    {
        UpdateScoreText();
    }

    public void ReduceScore(int amount)
    {
        finalScore -= amount;
        if (finalScore < 0) finalScore = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = finalScore.ToString() + " POINTS";
    }
}
