using UnityEngine;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        finalScoreText.text = ScoreManager.finalScore.ToString();
    }
}
