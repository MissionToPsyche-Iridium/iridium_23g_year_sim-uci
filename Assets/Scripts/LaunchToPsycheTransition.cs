using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Launch1 : MonoBehaviour
{
    public float transitionTime = 5f;
    public GDTFadeEffect fadeEffect;

    void Start()
    {
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
}
