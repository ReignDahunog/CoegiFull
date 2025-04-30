using UnityEngine;
using UnityEngine.Rendering;

public class CameraPan : MonoBehaviour
{
    public GameObject focusKotse;
    public float distance = 5f;
    public float height = 2f;
    public float dampening = 1f;
    public float h2 = 0f;
    public float d2 = 0f;
    public float l = 0f;

    private int camMode = 0;
    void Update() {
        if (Input.GetKeyDown(KeyCode.C))
        {
            camMode = (camMode + 1) % 2;
        }

        switch (camMode) {
            case 1:
                transform.position = focusKotse.transform.position + focusKotse.transform.TransformDirection(new Vector3(l, h2, d2));
                transform.rotation = focusKotse.transform.rotation;
                Camera.main.fieldOfView = 52f;
                break;
            default:
                transform.localPosition = focusKotse.transform.position + focusKotse.transform.TransformDirection(new Vector3(0f, height, -distance));
                transform.LookAt(focusKotse.transform);
                Camera.main.fieldOfView = 60f;
                break;
        }

    }
}
