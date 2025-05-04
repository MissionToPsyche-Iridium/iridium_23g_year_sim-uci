using UnityEngine;

public class ProximityTransparency : MonoBehaviour
{
    public float transparencyThreshold = 10f; // Distance where transparency starts
    public float transparencySpeed = 1f; // Speed of fading
    public Camera mainCamera;
    public Material opaque;
    public Material transparent;

    private float _alpha = 1f;
    private Renderer _renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the player and camera
        float distance = Vector3.Distance(transform.position, mainCamera.transform.position);

        // Determine the transparency level based on distance
        if (distance < transparencyThreshold) {
            _alpha = Mathf.Lerp(_alpha, 0f, transparencySpeed * Time.deltaTime); // Fade to transparent
        }
        else {
            _alpha = Mathf.Lerp(_alpha, 1f, transparencySpeed * Time.deltaTime); // Fade to opaque
        }

        if (_alpha > 0.9) {
            SetMaterial(false);
        }
        else {
            SetMaterial(true);
        }
        // Debug.Log(_alpha);

        // Set the material's alpha value
        Color color = _renderer.material.color;
        color.a = _alpha;
        _renderer.material.color = color;
    }

    public void SetMaterial(bool fading) {
        if (fading) {
            _renderer.material = transparent;
        } else {
            _renderer.material = opaque;
        }
    }
}
