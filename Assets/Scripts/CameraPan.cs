using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public GameObject focusKotse;
    public float distance = 5.0f;
    public float height = 1.5f;
    public float swayAmount = 0.1f;
    public float swaySmoothTime = 0.1f; // Smoothing time for sway movement

    public float h2 = 0f;
    public float d2 = 0f;
    public float l = 0f;

    private int camMode = 0;
    private float currentSwayX = 0f;
    private float swayVelocity = 0f;   // Used by SmoothDamp
    private Rigidbody carRb;

    void Start()
    {
        if (focusKotse != null)
            carRb = focusKotse.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camMode = (camMode + 1) % 2;
            currentSwayX = 0f;
            swayVelocity = 0f;
        }
    }

    void LateUpdate()
    {
        if (focusKotse == null) return;

        switch (camMode)
        {
            case 1:
                // First person, no sway
                transform.position = focusKotse.transform.position + focusKotse.transform.TransformDirection(new Vector3(l, h2, d2));
                transform.rotation = focusKotse.transform.rotation;
                Camera.main.fieldOfView = 52f;
                break;

            default:
                Vector3 localVelocity = Vector3.zero;
                if (carRb != null)
                {
                    localVelocity = focusKotse.transform.InverseTransformDirection(carRb.linearVelocity);
                }

                // Deadzone threshold to avoid jitter from small inputs
                float deadzone = 0.05f;
                float targetSwayInput = Mathf.Clamp(localVelocity.x, -0.5f, 0.5f);

                if (Mathf.Abs(targetSwayInput) < deadzone)
                    targetSwayInput = 0f;

                float targetSway = targetSwayInput * swayAmount;

                // Smoothly interpolate current sway towards target sway
                currentSwayX = Mathf.SmoothDamp(currentSwayX, targetSway, ref swayVelocity, swaySmoothTime);

                Vector3 cameraOffset = new Vector3(currentSwayX, height, -distance);
                transform.position = focusKotse.transform.position + focusKotse.transform.TransformDirection(cameraOffset);

                Vector3 lookTarget = focusKotse.transform.position + Vector3.up * height * 0.5f;
                transform.LookAt(lookTarget);

                Camera.main.fieldOfView = 60f;
                break;
        }
    }
}
