using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIReplay : MonoBehaviour
{
    public ReplayManager replayManager; // Assign this in the inspector

    // This method will be called when you want to go to the replay scene
    public void GoToReplay()
    {
        if (replayManager != null)
        {
            // Save recorded data to static storage (so it persists when switching scenes)
            ReplayStorage.StoredRecords = new List<ActionReplayRecord>(replayManager.GetRecordedData());

            // Load the replay scene
            SceneManager.LoadScene("EasyReplay", LoadSceneMode.Single);
        }
        else
        {
            Debug.LogWarning("ReplayManager reference not assigned in UIReplay.");
        }
    }
}
