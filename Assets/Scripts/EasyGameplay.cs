using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyGameplay : MonoBehaviour
{
    public void ToGame()
    {

        SceneManager.LoadSceneAsync("EasyGameplay");
    }
}
