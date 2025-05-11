using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {
	public GameObject settingsMenu;
	public CursorManager cursorManager;
	//public GameObject playerInputActions; // Potential fix for crash upon clicking "Return To Main Menu" in settings?
	public Scene currentScene;
	public bool isPaused;

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

	public void GoToMainMenu() {
		Time.timeScale = 1f;
		//playerInputActions.PlayerActionmap.Disable(); // (See variable assignment)
		SceneManager.LoadScene("TitleScreen");
		cursorManager.ToggleMenuCursor(true);
	}
}