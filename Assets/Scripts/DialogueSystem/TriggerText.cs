using UnityEngine;
using UnityEngine.Events;


public class TriggerText : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {
            Debug.Log("Entered");
            onTriggerEnter.Invoke();
        }
    }
}
