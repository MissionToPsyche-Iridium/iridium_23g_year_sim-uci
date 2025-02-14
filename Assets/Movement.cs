using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Movement : MonoBehaviour
{
    public Transform gravityTarget;
    public float power = 15000f;
    public float torque = 500f;
    public float gravity = 9.81f;

    private Vector3 DeskUp = Vector3.zero;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessGravity();
    }

    void ProcessInput() 
    {
        float Hor = Input.GetAxis("Horizontal");
        float Ver = Input.GetAxis("Vertical");

        Vector3 newPos = transform.position;
        newPos += transform.forward * Hor * 10 * Time.deltaTime;
        newPos += transform.right * Ver * 10 * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            newPos.y = (hit.point + Vector3.up * 10).y;
            DeskUp = hit.normal;
        }

        transform.position = newPos;
        transform.up = Vector3.Slerp(transform.up, DeskUp, 15 * Time.deltaTime);
    }

    void ProcessGravity() 
    {
        Vector3 diff = transform.position - gravityTarget.position;
        rb.AddForce(- diff.normalized * gravity * rb.mass);
        Debug.DrawRay(transform.position,  diff.normalized,  Color.red);
    }
}
