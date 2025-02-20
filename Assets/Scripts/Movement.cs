using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform gravityTarget;
    public float power = 15000f;
    public float torque = 500f;
    public float gravity = 9.81f;

    public bool autoOrient = false;
    public float autoOrientSpeed = 1f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ProcessInput();
        ProcessGravity();
    }

    void ProcessInput()
    {

    }

    void ProcessGravity()
    {   
        Vector3 diff = transform.position - gravityTarget.position;
        rb.AddForce(-diff.normalized * gravity * (rb.mass));

        if (autoOrient) {AutoOrient(-diff);}
    }

    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed * Time.deltaTime);
    }
}
