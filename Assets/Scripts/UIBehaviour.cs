using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

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
    public GameObject upgradesMenu;
    public GameObject researchMenu;
    public GameObject mapMenu;
    public GameObject solarSystemInfoIcon;
    public GameObject infoPanel;
    public TMP_Text infoTitle;
    public TMP_Text infoText;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera solarCamera;
    [SerializeField] private float fadeSpeed;
    private bool viewSolarSystem = false;
    private bool openSolarSystemView = true;
    private bool openUpgrades = true;
    private bool openResearch = true;
    private bool openMap = true;

    void Start() {
        mainCamera.enabled = true;
        solarCamera.enabled = false;
        upgradesMenu.SetActive(false);
        researchMenu.SetActive(false);
        mapMenu.SetActive(false);

        Color fadeColor = overlayFade.color;
        fadeColor.a = 0f; 
        overlayFade.color = fadeColor;
        overlayFade.gameObject.SetActive(false);
    }

    public void PauseGameUpgrade() {
        upgradesMenu.SetActive(true);
        Time.timeScale = 0f;

        if (openUpgrades) {
            infoPanel.SetActive(true);
            infoTitle.text = "Upgrades";
            infoText.text = "Placeholder text for first-time Upgrades open.";
            openUpgrades = false;
        } 
    }

    public void ResumeGameUpgrade() {
        upgradesMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGameResearch() {
        researchMenu.SetActive(true);
        Time.timeScale = 0f;

        if (openResearch) {
            infoPanel.SetActive(true);
            infoTitle.text = "Research";
            infoText.text = "Placeholder text for first-time Research open.";
            openResearch = false;
        } 
    }

    public void ResumeGameResearch() {
        researchMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGameMap() {
        mapMenu.SetActive(true);
        Time.timeScale = 0f;

        if (openMap) {
            infoPanel.SetActive(true);
            infoTitle.text = "Map";
            infoText.text = "Placeholder text for first-time Map open.";
            openMap = false;
        } 
    }

    public void ResumeGameMap() {
        mapMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void setCanvas() {
        solarSystemButton.SetActive(!viewSolarSystem);
        upgradesButton.SetActive(!viewSolarSystem);
        researchButton.SetActive(!viewSolarSystem);
        mapButton.SetActive(!viewSolarSystem);
        completionBar.SetActive(!viewSolarSystem);
        daysCounter.SetActive(!viewSolarSystem);
        missionsDropdown.SetActive(!viewSolarSystem);
        backButton.SetActive(viewSolarSystem);
        solarSystemInfoIcon.SetActive(viewSolarSystem);
        openSolarSystemViewInfo();
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

    public void openSolarSystemViewInfo() {
        if (openSolarSystemView) {
            infoPanel.SetActive(true);
            infoTitle.text = "Solar System View";
            infoText.text = "This is where you can view where Psyche is in the Solar System and keep track of its orbit anytime during your gameplay.";
            Time.timeScale = 0f;
            openSolarSystemView = false;
        } else {
            infoPanel.SetActive(false);
        }
    }

    public void SSVInfoIconClick() {
        openSolarSystemView = true;
        restartSolarSystemViewInfoIconAnimation();
        openSolarSystemViewInfo();
    }

    public void closeInfoPanel() {
        Time.timeScale = 1f;
        StartCoroutine(waitButtonAnimation(infoPanel));
    }

    public void restartSolarSystemViewInfoIconAnimation() {
        Animator iconAnimator = solarSystemInfoIcon.GetComponent<Animator>();
        iconAnimator.Play("Normal", -1, 0f);
    }

    IEnumerator waitButtonAnimation(GameObject panel) {
        yield return new WaitForSecondsRealtime(0.6f);
        panel.SetActive(false);
    }
}