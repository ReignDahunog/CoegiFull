using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText;

    public static int drivingScore = 10000;
    public static int parkingScore = 10000;
    public static int steeringScore = 10000;

    void Start()
    {
        UpdateScoreText();
    }

    public void ReduceDrivingScore(int amount)
    {
        drivingScore = Mathf.Max(0, drivingScore - amount);
        UpdateScoreText();
    }

    public void ReduceParkingScore(int amount)
    {
        parkingScore = Mathf.Max(0, parkingScore - amount);
        UpdateScoreText();
    }

    public void ReduceSteeringScore(int amount)
    {
        steeringScore = Mathf.Max(0, steeringScore - amount);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (totalScoreText != null)
        {
            totalScoreText.text = GetTotalScore().ToString();
        }
    }

    public static int GetDrivingScore() => drivingScore;
    public static int GetParkingScore() => parkingScore;
    public static int GetSteeringScore() => steeringScore;
    public static int GetTotalScore() => drivingScore + parkingScore + steeringScore;
}
