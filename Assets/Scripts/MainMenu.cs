using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ToGameMenu()
    {
        SceneManager.LoadScene(1); // Replace '1' with your scene index or name if needed
    }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Call the public method to load the scene
            ToGameMenu();
        }
    }
}