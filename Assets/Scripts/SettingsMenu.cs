using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {
	public GameObject settingsMenu;
	public CursorManager cursorManager;
	public bool isPaused;

	// Start is called before the first frame update
	void Start() { settingsMenu.SetActive(false); }

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

		cursorManager.ToggleMenuCursor(true);
	}

	public void ResumeGame() {
		settingsMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;

		cursorManager.ToggleMenuCursor(false);
	}

	public void GoToMainMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("TitleScreen");

		cursorManager.ToggleMenuCursor(false);
	}
}