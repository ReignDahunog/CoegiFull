using UnityEngine;
using UnityEngine.SceneManagement;

public class DrivingCourseButtonHandler : MonoBehaviour
{
    public void LoadDrivingCourseScene()
    {
        SceneManager.LoadScene("DrivingCourse");
    }
}
