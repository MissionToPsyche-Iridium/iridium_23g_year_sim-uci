using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject popUpPrefab;
    public GameObject canvasObject;

    public void CreatePopUp(string message)
    {
        GameObject createdPopUpObject = Instantiate(popUpPrefab, canvasObject.transform);
        createdPopUpObject.GetComponent<PopUp>().setPopUpText(message);
        StartCoroutine(SlideInAndOut(createdPopUpObject));
    }

    private IEnumerator SlideInAndOut(GameObject popup)
    {
        RectTransform canvasRect = canvasObject.GetComponent<RectTransform>();
        RectTransform rect = popup.GetComponent<RectTransform>();

        float canvasWidth = canvasRect.rect.width;

        // These Y values place it near top-right
        float targetY = -canvasRect.rect.height / 2 + 300;  // adjust as needed
        float targetX = canvasWidth / 2 - 225f;            // adjust for half width of popup

        Vector2 offscreenRight = new Vector2(canvasWidth, targetY);
        Vector2 onScreen = new Vector2(targetX, targetY);

        float duration = 0.5f;

        rect.anchoredPosition = offscreenRight;

        // Slide in
        yield return Slide(rect, offscreenRight, onScreen, duration);

        // Pause
        yield return new WaitForSeconds(2f);

        // Slide back out
        yield return Slide(rect, onScreen, offscreenRight, duration);

        // Destroy
        Destroy(popup);
    }

    private IEnumerator Slide(RectTransform rect, Vector2 from, Vector2 to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            rect.anchoredPosition = Vector2.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        rect.anchoredPosition = to;
    }
}
