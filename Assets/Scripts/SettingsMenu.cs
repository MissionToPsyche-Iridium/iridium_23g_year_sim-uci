using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {
	public GameObject settingsMenu;
	public GameObject warningPopUp;
	public GameObject helpMenuObject; // HelpMenu GameObject for set active true and false
	public HelpMenu helpMenu;	// Help Menu script to control button animation state resetting
	public CursorManager cursorManager;
	public Scene currentScene;
	public bool isPaused;
	public GameObject warningCancelButton;

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
        ResetButtonVisual(warningCancelButton);
	}

	public void WarningPopUpClose()
	{
		ResetButtonVisual(warningCancelButton);
		warningPopUp.SetActive(false);
	}

	public void HelpMenuOpen() {
		helpMenuObject.SetActive(true);
		cursorManager.ToggleMenuCursor(true);
		helpMenu.resetButtonStates();
		helpMenu.infoContent.gameObject.SetActive(false);
	}

	public void HelpMenuClose() {
		helpMenu.resetButtonStates();
		helpMenuObject.SetActive(false);
	}

	public void GoToMainMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("TitleScreen");
		cursorManager.ToggleMenuCursor(true);
	}

	private void ResetButtonVisual(GameObject buttonObject) { // Resets setting button animation back to Normal manually
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