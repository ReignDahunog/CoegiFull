using UnityEngine;
using TMPro;
using System.Collections;

public class SteeringCourseTutorial : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public GameObject finishLine;
    public AudioSource audioSource;

    public AudioClip welcomeClip;
    public AudioClip followRoadClip;

    private RectTransform tutorialRectTransform;
    private Vector2 originalPosition;

    void Start()
    {
        finishLine.SetActive(false);
        tutorialRectTransform = tutorialText.GetComponent<RectTransform>();
        originalPosition = tutorialRectTransform.anchoredPosition;
        StartCoroutine(RunSteeringCourseTutorial());
    }

    IEnumerator RunSteeringCourseTutorial()
    {
        yield return AnimateText("Welcome to the Steering Course!", welcomeClip);
        yield return new WaitForSeconds(2f);
        yield return AnimateText("Simply follow the road to finish!", followRoadClip);
        finishLine.SetActive(true);
    }

    IEnumerator AnimateText(string message, AudioClip clip)
    {
        tutorialText.text = message;

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
