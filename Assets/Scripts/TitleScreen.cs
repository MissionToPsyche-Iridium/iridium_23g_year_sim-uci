using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject storyPanel;
    public GameObject beginMissionButton;
    public GameObject settingsButton;
    public GameObject creditsButton;
    public GameObject psyche;
    public Image overlayFade;
    public Image psycheLogo;
    public Image yearOnPsycheLogo;

    [SerializeField] private float fadeSpeed;

    void Start() {
        creditsPanel.SetActive(false);
        storyPanel.SetActive(false);
        overlayFade.gameObject.SetActive(false);
        beginMissionButton.SetActive(true);
        settingsButton.SetActive(true);
        creditsButton.SetActive(true);
        psyche.SetActive(true);
        psycheLogo.gameObject.SetActive(true);
        yearOnPsycheLogo.gameObject.SetActive(true);
    }

    public void NewMission() {
        StorySetupTransition();
    }

    public void ContinueMission() {

    }

    public void OpenCredits() {
        creditsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseCreditsMenu() {
        Time.timeScale = 1f;
        StartCoroutine(waitButtonAnimation(creditsPanel));
    }

    IEnumerator waitButtonAnimation(GameObject panel) {
        yield return new WaitForSecondsRealtime(0.6f);
        panel.SetActive(false);
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

    public void StorySetupTransition() {
        StartCoroutine(StoryTransition());
    }

    IEnumerator StoryTransition() {
        overlayFade.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1)); // Fade Out

        storyPanel.SetActive(true);
        beginMissionButton.SetActive(false);
        settingsButton.SetActive(false);
        creditsButton.SetActive(false);
        psyche.SetActive(false);
        psycheLogo.gameObject.SetActive(false);
        yearOnPsycheLogo.gameObject.SetActive(false);

        yield return StartCoroutine(Fade(0)); // Fade In
        overlayFade.gameObject.SetActive(false);
    }

    public void CloseStory() {
        fadeOut();
        SceneManager.LoadSceneAsync("LaunchScene");
    }

    IEnumerator fadeOut() {
        overlayFade.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1)); // Fade Out
    }
}
