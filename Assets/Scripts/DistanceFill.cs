using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DistanceFill : MonoBehaviour {
    [SerializeField] public Image bar;
    [SerializeField] public TMP_Text distanceText;
    [SerializeField] public float maxDistance = 100f;
    [SerializeField] public int timeMilliseconds = 10000;
    [SerializeField] public int startDelayMilliseconds = 0; // optional delay before filling
    [SerializeField] public bool useStartDelay = true;

    private float totalSeconds;
    private float speedPerSecond;
    private float distance = 0f;
    private bool isFilling = false;

    void Start() {
        totalSeconds = timeMilliseconds / 1000f;
        speedPerSecond = maxDistance / totalSeconds;

        if (useStartDelay && startDelayMilliseconds > 0) {
            StartCoroutine(StartFillingAfterDelay(startDelayMilliseconds));
        } else {
            isFilling = true;
        }
    }

    void Update() {
        if (!isFilling) return;

        if (distance < maxDistance) {
            distance += speedPerSecond * Time.deltaTime;
            if (distance > maxDistance) distance = maxDistance;
        }

        updateBarProgress();
        updateDistanceText();
    }

    IEnumerator StartFillingAfterDelay(int delayMs) {
        yield return new WaitForSeconds(delayMs / 1000f);
        isFilling = true;
    }

    public void updateBarProgress() {
        bar.fillAmount = distance / maxDistance;
    }

    public void updateDistanceText() {
        distanceText.text = $"~ {distance:F1} miles";
    }
}
