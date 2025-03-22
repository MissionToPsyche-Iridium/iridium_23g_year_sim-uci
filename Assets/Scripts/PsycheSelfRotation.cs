using UnityEngine;

public class PsycheSelfRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;
    public GameObject psyche;

    // Update is called once per frame
    void Update()
    {
        if (psyche != null) {
            psyche.transform.Rotate(rotation * speed * Time.deltaTime);
        } else {
            Debug.LogError("ERROR: Psyche not found.");
        }
    }
}
