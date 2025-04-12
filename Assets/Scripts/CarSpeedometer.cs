using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public Rigidbody target;
    public float maxSpeed = 0.0f;

    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public TextMeshProUGUI speedLabel;
    public RectTransform arrow;

    private float speed = 0.0f;
    private float previousSpeed = 0.0f;

    private void Update()
    {
        // Calculate the speed in km/h
        float currentSpeed = target.linearVelocity.magnitude * 3.6f;

        // Check if the car is moving forward by using the dot product
        bool isMovingForward = Vector3.Dot(target.linearVelocity, target.transform.forward) > 0;

        // Only update the speed if the car is moving forward and not decelerating
        if (isMovingForward && currentSpeed >= previousSpeed)
        {
            speed = currentSpeed;

            // Update the speed label text
            if (speedLabel != null)
                speedLabel.text = ((int)speed) + " KM/H";

            // Update the arrow's rotation based on the speed
            if (arrow != null)
                arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
        }
        else if (!isMovingForward)
        {
            // Set the speed label to 0 if moving backward
            if (speedLabel != null)
                speedLabel.text = "0 KM/H";

            // Set the arrow to the minimum angle
            if (arrow != null)
                arrow.localEulerAngles = new Vector3(0, 0, minSpeedArrowAngle);
        }

        // Store the current speed as previous speed for the next frame
        previousSpeed = currentSpeed;
    }
}
