using UnityEngine;

public class CarInput : MonoBehaviour
{
    public float throttle;
    public float steer;
    public bool l;          // Headlights toggle
    public bool brake;
    public int gear;        // Current gear from Logitech shifter

    void Update()
    {
        if (Steering.Instance != null)
        {
            steer = Steering.Instance.SteeringAxis;
            throttle = Steering.Instance.GasInput;
            brake = Steering.Instance.BrakeInput > 0.1f;
            gear = Steering.Instance.CurrentGear;  // -1 for Reverse, 0 for Neutral, 1-6 forward gears
        }
        else
        {
            // Optional: fallback to keyboard controls if Steering not available
            steer = Input.GetAxis("Horizontal");
            throttle = Input.GetAxis("Vertical");
            brake = Input.GetKey(KeyCode.Space);
            gear = 0; // Neutral by default
        }

        l = Input.GetKeyDown(KeyCode.L);
    }
}
