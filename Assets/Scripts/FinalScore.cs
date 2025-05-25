using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    public TextMeshProUGUI drivingScoreText;
    public TextMeshProUGUI parkingScoreText;
    public TextMeshProUGUI steeringScoreText;

    public TextMeshProUGUI drivingRankText;
    public TextMeshProUGUI parkingRankText;
    public TextMeshProUGUI steeringRankText;

    void Start()
    {
        int driving = ScoreManager.GetDrivingScore();
        int parking = ScoreManager.GetParkingScore();
        int steering = ScoreManager.GetSteeringScore();

        drivingScoreText.text = driving.ToString();
        parkingScoreText.text = parking.ToString();
        steeringScoreText.text = steering.ToString();

        drivingRankText.text = GetRank(driving);
        parkingRankText.text = GetRank(parking);
        steeringRankText.text = GetRank(steering);
    }

    string GetRank(int score)
    {
        if (score == 10000) return "S";
        else if (score >= 9000) return "A";
        else if (score >= 8000) return "B";
        else if (score >= 7000) return "C";
        else if (score >= 6000) return "D";
        else if (score >= 5000) return "E";
        else return "F";
    }
}
