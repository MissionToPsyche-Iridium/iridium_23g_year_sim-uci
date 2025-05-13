using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LaunchToPsycheTransition : MonoBehaviour
{
    public float transitionTime = 5f;
    public Image overlayFade;
    public GDTFadeEffect fadeEffect;
    [SerializeField] private float fadeSpeed = 2f;

    void Start()
    {
        StartCoroutine(Fade(0));
        StartCoroutine(SwitchToScene("PsycheScene"));
    }

    IEnumerator SwitchToScene(string sceneName)
    {
        yield return new WaitForSeconds(transitionTime);

        if (fadeEffect != null)
        {
            fadeEffect.gameObject.SetActive(true);
            fadeEffect.StartEffect();  // Start the fading effect
        }

        yield return new WaitForSeconds(fadeEffect.timeEffect + fadeEffect.disableDelay); // Wait for fade to finish

        SceneManager.LoadScene(sceneName);
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
