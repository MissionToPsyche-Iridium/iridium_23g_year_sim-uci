using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIBehaviour : MonoBehaviour
{
    public GameObject solarSystemButton;
    public GameObject upgradesButton;
    public GameObject researchButton;
    public GameObject mapButton;
    public GameObject completionBar;
    public GameObject daysCounter;
    public GameObject missionsDropdown;
    public GameObject backButton;
    public Image overlayFade;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera solarCamera;
    [SerializeField] private float fadeSpeed;
    private bool viewSolarSystem = false;

    void Start() {
        mainCamera.enabled = true;
        solarCamera.enabled = false;

        Color fadeColor = overlayFade.color;
        fadeColor.a = 0f; 
        overlayFade.color = fadeColor;
        overlayFade.gameObject.SetActive(false);
    }

    public void setCanvas() {
        backButton.SetActive(viewSolarSystem);
        solarSystemButton.SetActive(!viewSolarSystem);
        upgradesButton.SetActive(!viewSolarSystem);
        researchButton.SetActive(!viewSolarSystem);
        mapButton.SetActive(!viewSolarSystem);
        completionBar.SetActive(!viewSolarSystem);
        daysCounter.SetActive(!viewSolarSystem);
        missionsDropdown.SetActive(!viewSolarSystem);
    }

    IEnumerator setSolarSystemView() {
        overlayFade.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1)); // Fade Out

        viewSolarSystem = true;
        mainCamera.enabled = false;
        solarCamera.enabled = true;

        setCanvas();

        yield return StartCoroutine(Fade(0)); // Fade In
        overlayFade.gameObject.SetActive(false);
    }

    IEnumerator setPsycheWorld() {
        overlayFade.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1)); // Fade Out

        viewSolarSystem = false;
        solarCamera.enabled = false;
        mainCamera.enabled = true;

        setCanvas();

        yield return StartCoroutine(Fade(0)); // Fade In
        overlayFade.gameObject.SetActive(false);
    }

    public void showSolarSystemView() {
        StartCoroutine(setSolarSystemView());
    }

    public void showPsycheWorldView() {
        StartCoroutine(setPsycheWorld());
    }

    IEnumerator Fade(float targetAlpha) {
        float startAlpha = overlayFade.color.a;
        float elapsedTime = 0f;
        Color currentColor = overlayFade.color;

        while (elapsedTime < fadeSpeed)
        {
            currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeSpeed);
            overlayFade.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentColor.a = targetAlpha;
        overlayFade.color = currentColor;
    }
}
