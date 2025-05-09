using UnityEngine;

public class MarsSizeChange : MonoBehaviour
{
    [SerializeField]
    private bool isDynamicScaler = false;

    [SerializeField]
    private Transform referencePlanet;

    [SerializeField]
    private float maxDistance = 3000f; // beyond this, planet shrinks to min scale

    [SerializeField]
    private float minScaleFactor = 0.2f; // how small it gets when far away

    private Vector3 originalScale;

    void Start()
    {
        if (isDynamicScaler)
            originalScale = transform.localScale;
    }

    void Update()
    {
        if (!isDynamicScaler || referencePlanet == null) return;

        float distance = Vector3.Distance(transform.position, referencePlanet.position);

        float t = Mathf.InverseLerp(0f, maxDistance, distance);
        float scale = Mathf.Lerp(1f, minScaleFactor, t);
        transform.localScale = originalScale * scale;
    }
}
