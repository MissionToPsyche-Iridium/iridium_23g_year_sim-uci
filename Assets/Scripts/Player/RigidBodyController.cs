using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : Movement
{
    float maxSpeed = 10f;
    public float amplitude = 2;
    public float speed = 1.5f;
    Rigidbody rig;

    void OnEnable()
    {
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rig.AddForce((movementVector + gravityDirection * gravityStrength + jumpVector) * Time.fixedDeltaTime, ForceMode.VelocityChange);
        rig.linearVelocity = Vector3.ClampMagnitude(rig.linearVelocity, maxSpeed); //Limits max player speed
        Vector3 p = model.transform.localPosition;
        p.y = amplitude * Mathf.Cos(Time.time * (speed + rig.linearVelocity.magnitude * 0.01f));
        model.transform.localPosition = Vector3.Lerp(model.transform.localPosition, p, 0.1f);        
    }
}