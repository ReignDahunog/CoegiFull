using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButtonHandler : MonoBehaviour
{
    public void LoadReplayScene()
    {
        SceneManager.LoadScene("EasyReplay"); 
    }
}
