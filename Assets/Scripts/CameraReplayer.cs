using System.Collections.Generic;
using System.IO;
using UnityEngine;
using CameraReplaySystem;

public class CameraReplayer : MonoBehaviour
{
    public Transform cameraTransform; // Assign in Inspector
    public Transform carTransform;    // Assign in Inspector

    private List<CameraRecord> cameraFrames;
    private List<CarRecord> carFrames;

    private float playbackStartTime;
    private float pausedTimeOffset = 0f;
    private float lastPausedTimestamp = 0f;

    private bool isPaused = false;
    private float manualSeekTime = 0f;
    private int currentIndex = 0;

    private void Start()
    {
        LoadRecording();
        playbackStartTime = Time.time;
    }

    private void Update()
    {
        if (cameraFrames == null || carFrames == null || currentIndex >= cameraFrames.Count - 1)
            return;

        float elapsed;

        if (isPaused)
        {
            elapsed = manualSeekTime;
        }
        else
        {
            elapsed = Time.time - playbackStartTime - pausedTimeOffset;
            manualSeekTime = elapsed;
        }

        while (currentIndex < cameraFrames.Count - 1 && cameraFrames[currentIndex + 1].timestamp <= elapsed)
        {
            currentIndex++;
        }

        CameraRecord camA = cameraFrames[currentIndex];
        CameraRecord camB = cameraFrames[Mathf.Min(currentIndex + 1, cameraFrames.Count - 1)];

        CarRecord carA = carFrames[currentIndex];
        CarRecord carB = carFrames[Mathf.Min(currentIndex + 1, carFrames.Count - 1)];

        float t = Mathf.InverseLerp(camA.timestamp, camB.timestamp, elapsed);

        cameraTransform.position = Vector3.Lerp(camA.position, camB.position, t);
        cameraTransform.rotation = Quaternion.Slerp(camA.rotation, camB.rotation, t);

        carTransform.position = Vector3.Lerp(carA.position, carB.position, t);
        carTransform.rotation = Quaternion.Slerp(carA.rotation, carB.rotation, t);
    }

    public void Play()
    {
        if (isPaused)
        {
            pausedTimeOffset += Time.time - lastPausedTimestamp;
            isPaused = false;
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            lastPausedTimestamp = Time.time;
            isPaused = true;
        }
    }

    public void Forward(float seconds)
    {
        manualSeekTime = Mathf.Min(manualSeekTime + seconds, cameraFrames[^1].timestamp);
        currentIndex = 0;
    }

    public void Backward(float seconds)
    {
        manualSeekTime = Mathf.Max(manualSeekTime - seconds, 0f);
        currentIndex = 0;
    }

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
}
