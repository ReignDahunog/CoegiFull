using UnityEngine;
using UnityEngine.SceneManagement;

public class SteeringCourseButtonHandler : MonoBehaviour
{
    public void LoadSteeringCourseScene()
    {
        SceneManager.LoadScene("SteeringCourse");
    }
}
