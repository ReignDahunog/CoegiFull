using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSceneforStarting : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadNextScene", 3f);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    
}
