using UnityEngine;

public class SaveReplayButton : MonoBehaviour
{
    public CameraRecorder recorder;

    public void Save()
    {
        if (recorder != null)
        {
            recorder.SaveRecording();
            Debug.Log("Recording saved from button.");
        }
        else
        {
            Debug.LogError("CameraRecorder reference is missing.");
        }
    }
}
