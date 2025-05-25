using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarInput))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LightingManager))]
[RequireComponent(typeof(Rigidbody))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    public CarInput im;
    public LightingManager lm;
    public UIManager uim;
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steerWheels;
    public List<GameObject> meshes;
    public float strengthMeter = 10000f;
    public float maxTurn = 20f;
    public Transform CM;
    public Rigidbody rb;
    public float brakeStrength;
    public List<GameObject> tailLights;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        im = GetComponent<CarInput>();
        rb = GetComponent<Rigidbody>();
        lm = GetComponent<LightingManager>();

        // Null check for Center of Mass
        if (CM)
        {
            rb.centerOfMass = CM.localPosition;
        }
        else
        {
            Debug.LogWarning("Center of Mass (CM) is not assigned.");
        }
    }

    void Update()
    {
        // Check if LightingManager (lm) is assigned
        if (im != null && lm != null && im.l)
        {
            lm.ControlHeadlights();
        }
        else if (lm == null)
        {
            Debug.LogWarning("LightingManager (lm) is not assigned.");
        }

        foreach (GameObject tl in tailLights)
        {
            if (tl != null && tl.GetComponent<Renderer>() != null)
            {
                Renderer renderer = tl.GetComponent<Renderer>();
                renderer.material.SetColor("_EmissionColor", im.brake ? new Color(0.5f, 0.111f, 0.111f) : Color.black);
            }
            else
            {
                Debug.LogWarning("Tail light GameObject is missing or has no Renderer component.");
            }
        }

        // Check if UIManager (uim) is assigned
        if (uim != null)
        {
            uim.changeText(transform.InverseTransformVector(rb.linearVelocity).z); // Corrected from `rb.linearVelocity` to `rb.velocity`
        }
        else
        {
            Debug.LogWarning("UIManager (uim) is not assigned.");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (WheelCollider wheel in throttleWheels)
        {
            if (wheel != null)
            {
                if (im.brake)
                {
                    wheel.motorTorque = 0f;
                    wheel.brakeTorque = brakeStrength * Time.deltaTime;
                }
                else
                {
                    wheel.motorTorque = strengthMeter * Time.deltaTime * im.throttle;
                    wheel.brakeTorque = 0f;
                }
            }
            else
            {
                Debug.LogWarning("Throttle wheel is not assigned.");
            }
        }

        foreach (GameObject wheel in steerWheels)
        {
            if (wheel != null && wheel.GetComponent<WheelCollider>() != null)
            {
                WheelCollider wc = wheel.GetComponent<WheelCollider>();
                wc.steerAngle = maxTurn * im.steer;
                wheel.transform.localEulerAngles = new Vector3(0f, im.steer * maxTurn, 0f);
            }
            else
            {
                Debug.LogWarning("Steer wheel or its WheelCollider is not assigned.");
            }
        }

        foreach (GameObject mesh in meshes)
        {
            if (mesh != null)
            {
                mesh.transform.Rotate(rb.linearVelocity.magnitude * (transform.InverseTransformDirection(rb.linearVelocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.37f), 0f, 0f);
            }
            else
            {
                Debug.LogWarning("Mesh GameObject is not assigned.");
            }
        }
    }
}
