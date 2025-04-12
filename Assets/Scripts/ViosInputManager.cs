using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViosInputManager : MonoBehaviour
{
    public float throttle;
    public float steer;
    public bool l;
    public bool brake;

    public Transform steeringWheel; // Assign in Inspector
    public float maxSteeringAngle = 180f; // Adjust as needed

    void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        l = Input.GetKeyDown(KeyCode.L);
        brake = Input.GetKey(KeyCode.Space);

        RotateSteeringWheel();
    }

    void RotateSteeringWheel()
    {
        if (steeringWheel != null)
        {
            float rotationAngle = steer * maxSteeringAngle;
            steeringWheel.localRotation = Quaternion.Euler(-14.578f, 180f, rotationAngle); // Inverted Z-axis rotation
        }
    }
}
