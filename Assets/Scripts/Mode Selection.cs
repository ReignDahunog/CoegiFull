using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ModeSelection : MonoBehaviour
{
    public void EasyMode()
    {
        SceneManager.LoadSceneAsync("Easy");
    }

    public void IntermidiateMode()
    {
        SceneManager.LoadSceneAsync("Intermidiate");
    }

    public void AdvanceMode()
    {
        SceneManager.LoadSceneAsync("Advance");
    }
}

