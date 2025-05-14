using UnityEngine;

public class RocketLookAtTarget : MonoBehaviour
{
    public Transform target;    
    private float originalRotX = 0f;
    private float originalRotY = -180f;
    
    void Start()
    {
        originalRotX = transform.rotation.eulerAngles.x;
        originalRotY = transform.rotation.eulerAngles.y;
    }
    
    void Update()
    {
        if (target == null)
            return;
            
        Vector3 direction = target.position - transform.position;
        
        direction.z = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Quaternion rotation = Quaternion.Euler(originalRotX, originalRotY, angle - 90f);
        transform.rotation = rotation;
    }
}