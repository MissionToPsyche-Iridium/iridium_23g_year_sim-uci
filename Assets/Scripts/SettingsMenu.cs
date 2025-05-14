using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {
	public GameObject settingsMenu;
	public GameObject warningPopUp;
	public CursorManager cursorManager;
	//public GameObject playerInputActions; // Potential fix for crash upon clicking "Return To Main Menu" in settings?
	public Scene currentScene;
	public bool isPaused;
	public GameObject cancelButton;

	// Start is called before the first frame update
	void Start() {
		settingsMenu.SetActive(false);
		currentScene = SceneManager.GetActiveScene();
	}

	void Update() {
		// Pause with keyboard ("Escape" key)
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (isPaused) {
				ResumeGame();
			} else { PauseGame(); }
		}
	}

	public void PauseGame() {
		settingsMenu.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
		if (currentScene.name != "TitleScreen") {
			cursorManager.ToggleMenuCursor(true);
		}
	}

	public void ResumeGame() {
		settingsMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
		if (currentScene.name != "TitleScreen") {
			cursorManager.ToggleMenuCursor(false);
		}
	}

	public void WarningPopUpOpen() {
		warningPopUp.SetActive(true);
		cursorManager.ToggleMenuCursor(true);

        // Force Button to reset visually and logically
        ResetButtonVisual(cancelButton);
	}

	public void WarningPopUpClose() {
		ResetButtonVisual(cancelButton);
        warningPopUp.SetActive(false);
	}

	public void GoToMainMenu() {
		Time.timeScale = 1f;
		//playerInputActions.PlayerActionmap.Disable(); // (See variable assignment)
		SceneManager.LoadScene("TitleScreen");
		cursorManager.ToggleMenuCursor(true);
	}

	private void ResetButtonVisual(GameObject buttonObject) {
		var button = buttonObject.GetComponent<Button>();
		if (button == null) return;

		Animator animator = button.GetComponent<Animator>();
		if (animator != null)
		{
			animator.Play("Normal", 0, 0);
			button.targetGraphic.color = Color.white;
			button.transform.localScale = Vector3.one;
		}
}

}