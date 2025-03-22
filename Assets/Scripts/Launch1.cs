using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Launch1 : MonoBehaviour
{
    public float transitionTime = 5f;
    public GDTFadeEffect fadeEffect; // Change type to GDTFadeEffect

    void Start()
    {
        StartCoroutine(SwitchToScene("LaunchScene2"));
    }

    IEnumerator SwitchToScene(string sceneName)
    {
        yield return new WaitForSeconds(transitionTime);

        if (fadeEffect != null)
        {
            fadeEffect.gameObject.SetActive(true);  // Ensure it's active
            fadeEffect.StartEffect();  // Start the fading effect
        }

        yield return new WaitForSeconds(fadeEffect.timeEffect + fadeEffect.disableDelay); // Wait for fade to finish

        SceneManager.LoadScene(sceneName);
    }
}
