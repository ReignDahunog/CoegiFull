using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ModeSelection : MonoBehaviour
{
    public void EasyMode()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void IntermidiateMode()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void AdvanceMode()
    {
        SceneManager.LoadSceneAsync(5);
    }
}

