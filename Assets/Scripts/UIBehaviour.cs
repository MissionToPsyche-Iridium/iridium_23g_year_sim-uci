using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour {
	[SerializeField] private float fadeSpeed;
	[SerializeField] private Camera mainCamera; // NOTE: What is "Main Camera"? Does this refer to the "Free Look Camera"? 
	[SerializeField] private Camera solarCamera; // NOTE: What does this mean? The camera for the solar system view?

	public GameObject completionBar;
	public GameObject daysCounter;
	public TMP_Text daysCounterTime;
	public GameObject solarSystemButton;
	public GameObject settingsButton;
	public GameObject upgradesButton;
	public GameObject upgradesMenu;
	public GameObject researchMenu;
	public GameObject researchButton;
	/// Uncomment to implement:
	//public GameObject mapButton;
	//public GameObject mapMenu;
	public GameObject missionsDropdown;
	public GameObject backButton;
	public GameObject infoPanel;
	public CursorManager cursorManager;

	public Image overlayFade;
	public TMP_Text infoTitle;
	public TMP_Text infoText;
	private bool viewSolarSystem = false;
	public float days = 1828f;
	private int clickCount = 0;
	private string[] tutorialTitle;
	private string[] tutorialText;
	public bool? tutorialOn = true;
	private GameObject[] UI;

	void Start() {
		mainCamera.enabled = true;
		solarCamera.enabled = false;
		upgradesMenu.SetActive(false);
		researchMenu.SetActive(false);
		// mapMenu.SetActive(false); // Uncomment to implement

		Color fadeColor = overlayFade.color;
		fadeColor.a = 0f; 
		overlayFade.color = fadeColor;
		overlayFade.gameObject.SetActive(false);
		UI = new GameObject[] { completionBar, daysCounter, solarSystemButton, upgradesButton, researchButton, missionsDropdown };
		tutorialTitle = new string[] { "COMPLETION BAR", "DAYS COUNTDOWN", "SOLAR SYSTEM VIEW", "UPGRADES", "RESEARCH", "MISSIONS", "END TUTORIAL" };
		tutorialText = new string[] { "This will display how much you've completed your mission. This includes gathering all research papers, maxing all upgrades, and completing mini missions.",
									"Psyche has 1828 days in a year. For the sake of gameplay, each day is a second in real life. Complete your mission before time is up. Time stops when Upgrades, Research, or Settings is open.",
									"This is where you can view where Psyche is in the Solar System and keep track of its orbit in the year anytime during your gameplay.",
									"Gather minerals and use them to upgrade your Rover throughout the game! Max out upgrades before the game ends to complete your mission.",
									"Generate all research papers that you will get from discovering something new about Psyche.",
									"Sub-missions will help guide you to completing your main mission: Gather as much data from Psyche as you can in a year. Complete them all to complete the game.",
									"That is all. Goodluck and have fun!"
									};
		Time.timeScale = 0f;
	}

	void Update() {
		if (tutorialOn != null) {
			Tutorial();
		}
		else if (days > 0 && Time.timeScale > 0) {
			days -= Time.deltaTime;
			UpdateDaysCounter();
		} else if (days <= 0) {
			EndGame();
		}
	}

	void EndTutorial() {
		UI[UI.Length - 1].transform.SetSiblingIndex(infoPanel.transform.GetSiblingIndex() - 1);
		infoTitle.text = tutorialTitle[tutorialTitle.Length - 1];
		infoText.text = tutorialText[tutorialTitle.Length - 1];
		tutorialOn = false;
		cursorManager.ToggleMenuCursor(false);
	}

	void Tutorial() {
		cursorManager.ToggleMenuCursor(true);
		if (clickCount < UI.Length && Input.GetMouseButtonDown(0)) {
			ShowNextInteractable();
		}
		else if (clickCount >= UI.Length && Input.GetMouseButtonDown(0) && tutorialOn == true) {
			EndTutorial();
		}
		else if (tutorialOn == false && Input.GetMouseButtonDown(0)) {
			infoPanel.SetActive(false);
			Time.timeScale = 1f;
			tutorialOn = null;
			cursorManager.ToggleMenuCursor(false);
		}
	}

	void ShowNextInteractable() {
		if (clickCount == 0) {
			UI[clickCount].transform.SetSiblingIndex(infoPanel.transform.GetSiblingIndex() + 1);
		} 
		else {
			UI[clickCount - 1].transform.SetSiblingIndex(infoPanel.transform.GetSiblingIndex() - 1);
			UI[clickCount].transform.SetSiblingIndex(infoPanel.transform.GetSiblingIndex() + 1);
		}
		infoTitle.text = tutorialTitle[clickCount];
		infoText.text = tutorialText[clickCount];
		clickCount++;
	}

	public void UpdateDaysCounter() {
		int seconds = Mathf.FloorToInt(days);
		daysCounterTime.text = days.ToString("0000");
	}

	public void EndGame() {
		SceneManager.LoadScene("TitleScreen");
		cursorManager.ToggleMenuCursor(true);
	}

	public void PauseGameUpgrade() {
		upgradesMenu.SetActive(true);
		Time.timeScale = 0f;
		cursorManager.ToggleMenuCursor(true);
	}

	public void ResumeGameUpgrade() {
		upgradesMenu.SetActive(false);
		Time.timeScale = 1f;
		cursorManager.ToggleMenuCursor(false);
	}

	public void PauseGameResearch() {
		researchMenu.SetActive(true);
		Time.timeScale = 0f;
		cursorManager.ToggleMenuCursor(true);
	}

	public void ResumeGameResearch() {
		researchMenu.SetActive(false);
		Time.timeScale = 1f;
		cursorManager.ToggleMenuCursor(false);
	}

	//public void PauseGameMap() { // Uncomment to implement
	//    mapMenu.SetActive(true);
	//    Time.timeScale = 0f;
	//    cursorManager.ToggleMenuCursor(true);
	//}

	//public void ResumeGameMap() {
	//    mapMenu.SetActive(false);
	//    Time.timeScale = 1f;
	//    cursorManager.ToggleMenuCursor(false);
	//}

	public void setCanvas() {
		solarSystemButton.SetActive(!viewSolarSystem);
		upgradesButton.SetActive(!viewSolarSystem);
		researchButton.SetActive(!viewSolarSystem);
		// mapButton.SetActive(!viewSolarSystem);
		completionBar.SetActive(!viewSolarSystem);
		daysCounter.SetActive(!viewSolarSystem);
		missionsDropdown.SetActive(!viewSolarSystem);
		settingsButton.SetActive(!viewSolarSystem);
		backButton.SetActive(viewSolarSystem);
		cursorManager.ToggleMenuCursor(viewSolarSystem);
	}

	IEnumerator setSolarSystemView() {
		overlayFade.gameObject.SetActive(true);
		yield return StartCoroutine(Fade(1)); // Fade screen out

		viewSolarSystem = true;
		mainCamera.enabled = false;
		solarCamera.enabled = true;

		setCanvas();

		yield return StartCoroutine(Fade(0)); // Fade screen in
		overlayFade.gameObject.SetActive(false);
	}
	public void showSolarSystemView() { // NOTE: This function isn't called. Why?
		StartCoroutine(setSolarSystemView());
	}

	IEnumerator setPsycheWorld() { // "set" is a little vague. Can we use more clear language? It's okay if it becomes a little more verbose.
		overlayFade.gameObject.SetActive(true);
		yield return StartCoroutine(Fade(1)); // Fade screen out

		viewSolarSystem = false;
		solarCamera.enabled = false;
		mainCamera.enabled = true;

		setCanvas();

		yield return StartCoroutine(Fade(0)); // Fade screen in
		overlayFade.gameObject.SetActive(false);
	}
	public void showPsycheWorldView() { // NOTE: This function isn't called. Why?
		StartCoroutine(setPsycheWorld());
	} 

	IEnumerator Fade(float targetAlpha) {
		float startAlpha = overlayFade.color.a;
		float elapsedTime = 0f;
		Color currentColor = overlayFade.color;
		while (elapsedTime < fadeSpeed){
			currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeSpeed);
			overlayFade.color = currentColor;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		currentColor.a = targetAlpha;
		overlayFade.color = currentColor;
	}
}