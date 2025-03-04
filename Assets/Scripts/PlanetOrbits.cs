using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetOrbits : MonoBehaviour
{
    public Transform orbitingObject;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    void Start() {
        if (orbitingObject == null) {
            orbitActive = false;
            return;
        }

        TrailRenderer trail = orbitingObject.GetComponent<TrailRenderer>();
        if (trail != null) {
            trail.enabled = false;
        }
    
        setOrbitingObjectPosition();

        if (trail != null) {
        StartCoroutine(EnableTrailAfterFrame(trail));
    }
        StartCoroutine (AnimateOrbit());
    }

    void setOrbitingObjectPosition() {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }

    IEnumerator EnableTrailAfterFrame(TrailRenderer trail) {
        yield return null;
        trail.Clear();
        trail.enabled = true;
    }

    IEnumerator AnimateOrbit() {
        if (orbitPeriod <= 0.1f) {
            orbitPeriod = 0.1f;
        }
        float orbitSpeed = 1f / orbitPeriod;
        while (orbitActive) {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            setOrbitingObjectPosition();
            yield return null;
        }
    }
}
