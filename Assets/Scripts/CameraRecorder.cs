using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CameraReplaySystem;

public class CameraRecorder : MonoBehaviour
{
    public Transform carTransform; // Assign in Inspector

    private List<CameraRecord> recordedCameraFrames = new List<CameraRecord>();
    private List<CarRecord> recordedCarFrames = new List<CarRecord>();
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        float timestamp = Time.time - startTime;

        recordedCameraFrames.Add(new CameraRecord
        {
            position = transform.position,
            rotation = transform.rotation,
            timestamp = timestamp
        });

        recordedCarFrames.Add(new CarRecord
        {
            position = carTransform.position,
            rotation = carTransform.rotation,
            timestamp = timestamp
        });
    }

    private void OnDisable()
    {
        SaveRecording();
    }

    public void SaveRecording()
    {
        DrivingRecording recording = new DrivingRecording
        {
            cameraFrames = recordedCameraFrames,
            carFrames = recordedCarFrames
        };

        string json = JsonUtility.ToJson(recording);
        string path = Path.Combine(Application.persistentDataPath, "drivingReplay.json");
        File.WriteAllText(path, json);

        Debug.Log("Driving recording saved to: " + path);
    }
}
