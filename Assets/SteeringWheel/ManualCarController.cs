using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gearbox : MonoBehaviour
{
    public float maxSpeedPerGear = 40f;
    public float acceleration = 100f;
    public float deceleration = 80f;
    public float clutchEngageThreshold = 0.3f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var input = Steering.Instance;

        float gas = input.GasInput;
        float brake = input.BrakeInput;
        float clutch = input.ClutchInput;
        int gear = input.CurrentGear;

        float moveDirection = 0f;

        if (clutch < clutchEngageThreshold) // clutch released
        {
            if (gear > 0)
                moveDirection = 1f;
            else if (gear < 0)
                moveDirection = -1f;
        }

        Vector3 forward = transform.forward;
        float currentSpeed = Vector3.Dot(rb.linearVelocity, forward);

        float targetSpeed = currentSpeed;

        if (moveDirection != 0f)
        {
            float speedLimit = gear > 0 ? gear * maxSpeedPerGear : maxSpeedPerGear;

            targetSpeed += moveDirection * gas * acceleration * Time.fixedDeltaTime;
            targetSpeed = Mathf.Clamp(targetSpeed, -maxSpeedPerGear, speedLimit);

            rb.linearVelocity = forward * targetSpeed;
        }

        if (brake > 0.05f)
        {
            float decel = currentSpeed - brake * deceleration * Time.fixedDeltaTime;
            rb.linearVelocity = forward * Mathf.Clamp(decel, -maxSpeedPerGear, maxSpeedPerGear);
        }

        // Steering (using SteeringAxis)
        float turnAmount = input.SteeringAxis * 50f * Time.fixedDeltaTime;
        transform.Rotate(Vector3.up, turnAmount);
    }
}
