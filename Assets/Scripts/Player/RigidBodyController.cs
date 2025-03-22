using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : Movement
{
    float maxSpeed = 10f;
    Rigidbody rig;

    void OnEnable()
    {
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rig.AddForce((movementVector + gravityDirection*gravityStrength + jumpVector) * Time.fixedDeltaTime, ForceMode.VelocityChange);
        rig.linearVelocity = Vector3.ClampMagnitude(rig.linearVelocity, maxSpeed); //Limits max player speed
    }
}