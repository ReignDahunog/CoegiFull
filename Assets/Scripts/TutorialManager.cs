using UnityEngine;
using TMPro;
using System.Collections;

public class DrivingTutorial : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public GameObject finishLine;

    private bool hasSteered = false;
    private bool hasBraked = false;
    private bool hasChangedCam = false;
    private bool hasTurnedOnLights = false;

    private RectTransform tutorialRectTransform;
    private Vector2 originalPosition;

    void Start()
    {
        finishLine.SetActive(false);
        tutorialRectTransform = tutorialText.GetComponent<RectTransform>();
        originalPosition = tutorialRectTransform.anchoredPosition;
        StartCoroutine(AnimateText("Use WASD to steer."));
    }

    void Update()
    {
        if (!hasSteered && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            hasSteered = true;
            StartCoroutine(AnimateText("Press Space to brake."));
        }

        if (!hasBraked && Input.GetKeyDown(KeyCode.Space))
        {
            hasBraked = true;
            StartCoroutine(AnimateText("Press C to change camera."));
        }

        if (!hasChangedCam && Input.GetKeyDown(KeyCode.C))
        {
            hasChangedCam = true;
            StartCoroutine(AnimateText("Press L to turn on headlights."));
        }

        if (!hasTurnedOnLights && Input.GetKeyDown(KeyCode.L))
        {
            hasTurnedOnLights = true;
            StartCoroutine(AnimateText("Drive to the finish line to complete the tutorial."));
            finishLine.SetActive(true);
        }
    }

    IEnumerator AnimateText(string message)
    {
        tutorialText.text = message;

        RectTransform rect = tutorialText.GetComponent<RectTransform>();
        Vector2 startPos = new Vector2(0, Screen.height); // Slide in from top
        Vector2 endPos = originalPosition;

        float duration = 1f;
        float elapsed = 0f;

        rect.anchoredPosition = startPos;

        while (elapsed < duration)
        {
            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = endPos;
    }
}
