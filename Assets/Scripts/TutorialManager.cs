using UnityEngine;
using TMPro;

public class DrivingTutorial : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public GameObject finishLine; // Finish Line GameObject (disabled at start)

    private bool hasSteered = false;
    private bool hasBraked = false;
    private bool hasChangedCam = false;
    private bool hasTurnedOnLights = false;

    void Start()
    {
        finishLine.SetActive(false);
        UpdateTutorialText();
    }

    void Update()
    {
        if (!hasSteered && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            hasSteered = true;
            UpdateTutorialText();
        }

        if (!hasBraked && Input.GetKeyDown(KeyCode.Space))
        {
            hasBraked = true;
            UpdateTutorialText();
        }

        if (!hasChangedCam && Input.GetKeyDown(KeyCode.C))
        {
            hasChangedCam = true;
            UpdateTutorialText();
        }

        if (!hasTurnedOnLights && Input.GetKeyDown(KeyCode.L))
        {
            hasTurnedOnLights = true;
            UpdateTutorialText();
            EnableFinishLine();
        }
    }

    void UpdateTutorialText()
    {
        if (!hasSteered)
            tutorialText.text = "Use WASD to steer.";
        else if (!hasBraked)
            tutorialText.text = "Press Space to brake.";
        else if (!hasChangedCam)
            tutorialText.text = "Press C to change camera.";
        else if (!hasTurnedOnLights)
            tutorialText.text = "Press L to turn on headlights.";
    }

    void EnableFinishLine()
    {
        tutorialText.text = "Drive to the finish line to complete the tutorial.";
        finishLine.SetActive(true);
    }
}
