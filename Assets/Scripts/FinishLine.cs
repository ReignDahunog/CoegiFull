using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger entered by: " + collision.name);

        if (collision.CompareTag("Corolla_E180"))
        {
            Debug.Log("Correct tag detected. Loading next scene.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
