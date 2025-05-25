using UnityEngine;
using TMPro;
using System.Collections;

public class DrivingTutorial : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public GameObject finishLine;
    public AudioSource audioSource;

    // Audio clips for each step
    public AudioClip welcomeClip;
    public AudioClip wClip;
    public AudioClip aClip;
    public AudioClip dClip;
    public AudioClip sClip;
    public AudioClip brakeClip;
    public AudioClip camClip;
    public AudioClip lightClip;
    public AudioClip finishClip;

    private bool pressedW = false;
    private bool pressedA = false;
    private bool pressedD = false;
    private bool pressedS = false;

    private bool hasBraked = false;
    private bool hasChangedCam = false;
    private bool hasTurnedOnLights = false;
    private bool tutorialStarted = false;

    private RectTransform tutorialRectTransform;
    private Vector2 originalPosition;

    void Start()
    {
        finishLine.SetActive(false);
        tutorialRectTransform = tutorialText.GetComponent<RectTransform>();
        originalPosition = tutorialRectTransform.anchoredPosition;
        StartCoroutine(BeginTutorial());
    }

    void Update()
    {
        if (!tutorialStarted) return;

        if (!pressedW && Input.GetKeyDown(KeyCode.W))
        {
            pressedW = true;
            StartCoroutine(NextStep("Awesome! Now press A to steer left.", aClip));
        }
        else if (pressedW && !pressedA && Input.GetKeyDown(KeyCode.A))
        {
            pressedA = true;
            StartCoroutine(NextStep("Great! Press D to steer right.", dClip));
        }
        else if (pressedA && !pressedD && Input.GetKeyDown(KeyCode.D))
        {
            pressedD = true;
            StartCoroutine(NextStep("Nice moves! Press S to reverse.", sClip));
        }
        else if (pressedD && !pressedS && Input.GetKeyDown(KeyCode.S))
        {
            pressedS = true;
            StartCoroutine(NextStep("Well done! Hit Space to brake.", brakeClip));
        }
        else if (pressedS && !hasBraked && Input.GetKeyDown(KeyCode.Space))
        {
            hasBraked = true;
            StartCoroutine(NextStep("Cool! Now press C to switch cameras.", camClip));
        }
        else if (hasBraked && !hasChangedCam && Input.GetKeyDown(KeyCode.C))
        {
            hasChangedCam = true;
            StartCoroutine(NextStep("Nice! Hit L to turn on the headlights.", lightClip));
        }
        else if (hasChangedCam && !hasTurnedOnLights && Input.GetKeyDown(KeyCode.L))
        {
            hasTurnedOnLights = true;
            StartCoroutine(NextStep("You're all set! Drive to the finish line!", finishClip));
            finishLine.SetActive(true);
        }
    }

    IEnumerator BeginTutorial()
    {
        yield return AnimateText("Welcome to the driving tutorial!", welcomeClip);
        yield return new WaitForSeconds(2f);
        yield return AnimateText("Let's roll! Press W to move forward.", wClip);
        tutorialStarted = true;
    }

    IEnumerator NextStep(string message, AudioClip clip)
    {
        yield return AnimateText(message, clip);
    }

    IEnumerator AnimateText(string message, AudioClip clip)
    {
        tutorialText.text = message;

        // Play audio if available
        if (audioSource != null && clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }

        RectTransform rect = tutorialText.GetComponent<RectTransform>();
        Vector2 startPos = new Vector2(0, Screen.height);
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
