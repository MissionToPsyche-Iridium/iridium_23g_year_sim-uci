using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {
	public GameObject settingsMenu;
	public CursorManager cursorManager;
	public bool isPaused;

	void Start() { settingsMenu.SetActive(false); }

	void Update() {
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

		cursorManager.ToggleCursor(true);
		cursorManager.DisableCursorListeners();
	}

	public void ResumeGame() {
		settingsMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;

		cursorManager.ToggleCursor(false);
		cursorManager.EnableCursorListeners();
	}

	public void GoToMainMenu() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("TitleScreen");
	}
}