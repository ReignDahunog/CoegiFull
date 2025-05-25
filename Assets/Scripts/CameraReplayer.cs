using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CameraReplaySystem;

public class CameraReplayer : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform carTransform;

    [Header("UI")]
    public Slider progressBar;
    public TextMeshProUGUI startTimeTMP;
    public TextMeshProUGUI endTimeTMP;

    private List<CameraRecord> cameraFrames;
    private List<CarRecord> carFrames;

    private float playbackStartTime;
    private bool isPaused = false;
    private float seekTime = 0f;
    private int currentIndex = 0;

    private float recordingDuration;
    private bool isDraggingSlider = false;

    private void Start()
    {
        LoadRecording();

        if (cameraFrames == null || cameraFrames.Count < 2)
        {
            Debug.LogError("Not enough frames to play.");
            enabled = false;
            return;
        }

        recordingDuration = cameraFrames[^1].timestamp;

        if (progressBar != null)
        {
            progressBar.minValue = 0f;
            progressBar.maxValue = recordingDuration;
            progressBar.onValueChanged.AddListener(OnSliderValueChanged);
        }

        if (endTimeTMP != null)
            endTimeTMP.text = FormatTime(recordingDuration);

        playbackStartTime = Time.time;
    }

    private void Update()
    {
        if (cameraFrames == null || cameraFrames.Count < 2)
            return;

        float currentTime;

        if (isPaused)
        {
            currentTime = seekTime;
        }
        else if (isDraggingSlider)
        {
            return;
        }
        else
        {
            currentTime = Time.time - playbackStartTime;
            seekTime = Mathf.Clamp(currentTime, 0f, recordingDuration);
        }

        UpdatePlayback(seekTime);

        if (progressBar != null && !isDraggingSlider)
            progressBar.value = seekTime;

        if (startTimeTMP != null)
            startTimeTMP.text = FormatTime(seekTime);
    }

    private void UpdatePlayback(float currentTime)
    {
        for (int i = 0; i < cameraFrames.Count - 1; i++)
        {
            if (cameraFrames[i + 1].timestamp > currentTime)
            {
                currentIndex = i;
                break;
            }
        }

        CameraRecord camA = cameraFrames[currentIndex];
        CameraRecord camB = cameraFrames[Mathf.Min(currentIndex + 1, cameraFrames.Count - 1)];

        CarRecord carA = carFrames[currentIndex];
        CarRecord carB = carFrames[Mathf.Min(currentIndex + 1, carFrames.Count - 1)];

        float t = Mathf.InverseLerp(camA.timestamp, camB.timestamp, currentTime);

        cameraTransform.position = Vector3.Lerp(camA.position, camB.position, t);
        cameraTransform.rotation = Quaternion.Slerp(camA.rotation, camB.rotation, t);

        carTransform.position = Vector3.Lerp(carA.position, carB.position, t);
        carTransform.rotation = Quaternion.Slerp(carA.rotation, carB.rotation, t);
    }

    public void Play()
    {
        if (isPaused)
        {
            isPaused = false;
            playbackStartTime = Time.time - seekTime;
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
        }
    }

    public void Forward(float seconds)
    {
        seekTime = Mathf.Min(seekTime + seconds, recordingDuration);
        UpdatePlayback(seekTime);
        if (!isPaused)
            playbackStartTime = Time.time - seekTime;

        if (progressBar != null && !isDraggingSlider)
            progressBar.value = seekTime;

        if (startTimeTMP != null)
            startTimeTMP.text = FormatTime(seekTime);
    }

    public void Backward(float seconds)
    {
        seekTime = Mathf.Max(seekTime - seconds, 0f);
        UpdatePlayback(seekTime);
        if (!isPaused)
            playbackStartTime = Time.time - seekTime;

        if (progressBar != null && !isDraggingSlider)
            progressBar.value = seekTime;

        if (startTimeTMP != null)
            startTimeTMP.text = FormatTime(seekTime);
    }

    public void Forward2Sec() => Forward(2f);
    public void Backward2Sec() => Backward(2f);

    private void LoadRecording()
    {
        string path = Path.Combine(Application.persistentDataPath, "drivingReplay.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DrivingRecording recording = JsonUtility.FromJson<DrivingRecording>(json);
            cameraFrames = recording.cameraFrames;
            carFrames = recording.carFrames;

            Debug.Log("Replay loaded from: " + path);
        }
        else
        {
            Debug.LogError("Replay file not found at: " + path);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes}:{seconds:D2}";
    }

    public void OnBeginDragSlider() => isDraggingSlider = true;

    public void OnEndDragSlider()
    {
        isDraggingSlider = false;
        seekTime = progressBar.value;

        if (!isPaused)
        {
            playbackStartTime = Time.time - seekTime;
        }

        UpdatePlayback(seekTime);
    }

    public void OnSliderValueChanged(float value)
    {
        if (isDraggingSlider)
        {
            seekTime = value;

            if (startTimeTMP != null)
                startTimeTMP.text = FormatTime(seekTime);
        }
    }
}
