using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {
    public GameObject settingsMenu;
    public CameraBehavior cameraBehavior;
    public bool isPaused;

    void Start() { settingsMenu.SetActive(false); }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
        if (Time.timeScale == 0f) {
			cameraBehavior.ToggleCursor(true);
			cameraBehavior.DisableCursorListeners();
		} else {
			cameraBehavior.ToggleCursor(false);
			cameraBehavior.DisableCursorListeners();
		}
    }

    public void PauseGame() {
        settingsMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

		cameraBehavior.ToggleCursor(true);
        cameraBehavior.DisableCursorListeners();
    }

    public void ResumeGame() {
        settingsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

		cameraBehavior.ToggleCursor(false);
        cameraBehavior.EnableCursorListeners();
    }

    public void GoToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }
}