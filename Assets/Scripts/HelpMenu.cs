using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HelpMenu : MonoBehaviour {
    public Transform content;
    public CanvasGroup infoContent;
    public TMP_Text helpTitle;
    public Image helpImage;
    public TMP_Text helpText;

    void Start() {
        infoContent.gameObject.SetActive(false);
    }

    public void resetButtonStates() {
        foreach (Transform button in content) {
            ResetButtonVisual(button.gameObject);
        }
    }

    private void ResetButtonVisual(GameObject buttonObject) {
		var button = buttonObject.GetComponent<Button>();
		if (button == null) return;

		Animator animator = button.GetComponent<Animator>();
		if (animator != null) {
			animator.Play("Normal", 0, 0);
		}

        // Button Fill Image to 0
        Transform imageTransform = buttonObject.transform.Find("Fill");
        if (imageTransform != null) {
            Image fillImage = imageTransform.GetComponent<Image>();
            if (fillImage != null) {
                fillImage.fillAmount = 0;
            }
        }

        // Button Text back to white
        Transform textTransform = buttonObject.transform.Find("Name");
        if (textTransform != null) {
            TMP_Text buttonText = textTransform.GetComponent<TMP_Text>();
            if (buttonText != null) {
                buttonText.color = Color.white;
            }
        }
	}

    public void CursorButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Cursor";
    }

    public void MiningButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Mining";
    }
}