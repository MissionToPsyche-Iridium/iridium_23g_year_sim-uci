using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ImageTransition : MonoBehaviour
{
    public Image rocketImage;
    public Image satelliteImage;
    public float transitionTime = 5f; // Time to wait before switching images

    void Start()
    {
        // Initially show rocket image and hide satellite image
        rocketImage.gameObject.SetActive(true);
        satelliteImage.gameObject.SetActive(false);

        // Start the image transition
        StartCoroutine(SwitchImages());
    }

    IEnumerator SwitchImages()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(transitionTime);

        // Switch images
        rocketImage.gameObject.SetActive(false);
        satelliteImage.gameObject.SetActive(true);

        // Wait for 5 seconds
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadSceneAsync("PsycheScene");
    }
}
