using UnityEngine;

public class CarInput : MonoBehaviour
{
    public float throttle;
    public float steer;
    public bool l;
    public bool brake;
    // Update is called once per frame
    void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");

        l = Input.GetKeyDown(KeyCode.L);

        brake = Input.GetKey(KeyCode.Space);
    }
}
