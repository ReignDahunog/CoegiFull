using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoStart : MonoBehaviour
{
    public void Back_to()
    {
        SceneManager.LoadScene("Main");
    }
}
