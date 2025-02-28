using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 15;
    private Vector3 moveDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate() 
    {
        GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }
}
