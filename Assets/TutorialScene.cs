using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScene : MonoBehaviour
{
    public void ToTutorial()
    {

        SceneManager.LoadSceneAsync("Tutorial");
    }
}
