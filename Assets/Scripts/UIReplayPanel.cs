using UnityEngine;

public class ReplayUIController : MonoBehaviour
{
    public GameObject replayUIPanel; // Assign in Inspector

    public void ShowReplayUI()
    {
        replayUIPanel.SetActive(true);
    }
}
