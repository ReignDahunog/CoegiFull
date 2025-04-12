using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ViosInputManager))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(LightingManager))]
public class ViosController : MonoBehaviour {
    public ViosInputManager im;
    public LightingManager lm;
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steeringWheels;
    public List<GameObject> meshes;
    public float strengthCoefficient = 10000f;
    public float maxTurn = 20f;
    public Transform CM;
    public Rigidbody rb;
    public float brakeStrength;
    public List<GameObject> tailLights;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        im = GetComponent<ViosInputManager>();
        rb = GetComponent<Rigidbody>();

        if (CM)
        {
            rb.centerOfMass = CM.localPosition;
        }
    }

    void Update()
    {
        if (im.l)
        {
            lm.ControlHeadlights();
        }
        
        if (Input.GetKey(KeyCode.Space))
        foreach (GameObject tl in tailLights)
        {
                tl.GetComponent<Renderer>().material.SetColor("_EmissionColor", im.brake ? new Color (0.5f, 0.111f, 0.111f) : Color.black);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (WheelCollider wheel in throttleWheels)
        {
            if (im.brake)
            {
                wheel.motorTorque = 0f;
                wheel.brakeTorque = brakeStrength * Time.deltaTime;
            }
            else
            {
                wheel.motorTorque = strengthCoefficient * Time.deltaTime * im.throttle;
                wheel.brakeTorque = 0f;
            }
            wheel.motorTorque = strengthCoefficient * Time.deltaTime * im.throttle;
        }

        foreach (GameObject wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            wheel.transform.localEulerAngles = new Vector3(0f, im.steer * maxTurn, 0f);
        }

        foreach(GameObject mesh in meshes)
        {
            mesh.transform.Rotate(rb.linearVelocity.magnitude * (transform.InverseTransformDirection(rb.linearVelocity).z >= 0 ? 1 : -1) / (2 * Mathf.PI * 0.33f), 0f, 0f);
        }
    }
}
