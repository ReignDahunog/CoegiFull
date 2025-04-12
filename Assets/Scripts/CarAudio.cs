using UnityEngine;

public class CarAudio : MonoBehaviour
{
    Rigidbody rb;
    public AudioSource EngineSauce;

    public int GearShiftLength = 5; // Increase to prevent high jumps in pitch
    public float PitchBoost = 2.0f; // Increased pitch boost
    public float PitchRange = 2.5f; // Increased pitch range

    float Temp1;
    int Temp2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (EngineSauce == null)
        {
            Debug.LogError("EngineSauce is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (rb == null || EngineSauce == null || GearShiftLength <= 0)
            return;

        float Speed = rb.linearVelocity.magnitude;

        Temp1 = Speed / GearShiftLength;
        Temp2 = (int)Temp1;

        float Difference = Temp1 - Temp2;

        float newPitch = Mathf.Lerp(EngineSauce.pitch, (PitchRange * Difference) * PitchBoost, 0.1f); // Increased lerp factor

        if (float.IsNaN(newPitch) || float.IsInfinity(newPitch))
            newPitch = 1f; // Default safe pitch

        EngineSauce.pitch = Mathf.Clamp(newPitch, 0.5f, 5.0f); // Allowing higher pitch range
    }
}
