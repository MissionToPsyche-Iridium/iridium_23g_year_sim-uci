using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIBehaviour : MonoBehaviour
{
	[SerializeField] private float fadeSpeed;
	[SerializeField] private Camera playerCamera;
	[SerializeField] private Camera solarSystemCamera;
	[SerializeField] private UnityEvent tutorialDialogue;
	[SerializeField] private UnityEvent Day1200Dialogue;
	[SerializeField] private UnityEvent Day400Dialogue;
	[SerializeField] private UnityEvent SolarDialogue;
	public GameObject messages;
	public GameObject canvas;
	public GameObject missionsDropdown;
	public GameObject completionBar;
	public GameObject dupeCompletionBar; // Due to hierarchy difficulty with layout settings, a dupe image of the completion bar will be used ONLY for the tutorial.
	public GameObject daysCounter;
	public TMP_Text daysCounterTime;
	public GameObject solarSystemButton;
	public GameObject upgradesButton;
	public GameObject upgradesMenu;
	public GameObject researchMenu;
	public GameObject researchButton;
	public GameObject settingsButton;
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
	public string dialogueStatus = "landed";
	private GameObject[] UI;
	private float solarSystemTimeViewed = 0f;
	private bool hasTriggeredSolarReaction = false;
	public ResearchPaperLock paperLock;
	public PopUpManager popUpManager;
	private bool triggered1400 = false;
	private bool triggered400 = false;


	void Start()
	{
		playerCamera.enabled = true;
		solarSystemCamera.enabled = false;
		upgradesMenu.SetActive(false);
		researchMenu.SetActive(false);

		Color fadeColor = overlayFade.color;
		fadeColor.a = 0f;
		overlayFade.color = fadeColor;
		overlayFade.gameObject.SetActive(false);

		UI = new GameObject[] { missionsDropdown, dupeCompletionBar, daysCounter, solarSystemButton, upgradesButton, researchButton, settingsButton };
		tutorialTitle = new string[] { "MISSIONS", "PROGRESS BAR", "DAYS COUNTDOWN", "SOLAR SYSTEM VIEW", "UPGRADES", "RESEARCH", "SETTINGS", "CURSOR", "END TUTORIAL" };
		tutorialText = new string[] { "Missions will help guide you to completing your main mission: Gather as much data from Psyche as you can in a year. Complete them all to finish the game.",
									"This bar shows how much progress you have made to completing your Psyche mission. Completing missions, maxing upgrades, and generating research papers will increase your progress.",
									"Psyche has 1828 days in a year. For the sake of gameplay, each day is a second in real time. Complete your mission before time is up. Time stops when Upgrades, Research, or Settings screens are open.",
									"This is where you can view where Psyche is in the Solar System and keep track of its orbit in the year anytime during your gameplay.",
									"Gather minerals and use them to upgrade your Rover throughout the game! Max out upgrades before the game ends to complete your mission.",
									"Any research paper you generate can be found in here. Check in here anytime to read up any interesting facts you have found from exploring Psyche!",
									"Clicking the gear or tapping ESC will bring up the Settings menu. You can control volumne, text sizes, find hints about how to generate all research papers, and end game.",
									"Holding the Alt key (or Option key) will show the cursor to interact with the on-screen buttons.",
									"That is all. Goodluck and have fun!"
									};
		Time.timeScale = 1f;
	}

	void Update()
	{
		if (dialogueStatus == "landed")
		{
			cursorManager.ToggleMenuCursor(false);
			tutorialDialogue.Invoke();
			dialogueStatus = "started";
		}
		else if (dialogueStatus == "started")
		{
			if (!messages.activeSelf)
			{
				dialogueStatus = "finished";
			}
		}
		else if (tutorialOn != null)
		{
			Time.timeScale = 0f;
			cursorManager.ToggleMenuCursor(true);
			infoPanel.SetActive(true);
			Tutorial();
		}
		else if (days > 0 && Time.timeScale > 0)
		{
			days -= Time.deltaTime;
			UpdateDaysCounter();
			CheckDayMilestones();
			checkSolarDialogue();
			checkDayDialogue();
		}
		else if (days <= 0)
		{
			EndGame();
		}
	}

	void Tutorial()
	{
		cursorManager.ToggleMenuCursor(true);
		if (clickCount < UI.Length && Input.GetMouseButtonDown(0))
		{
			ShowNextInteractable();
		}
		else if (clickCount >= UI.Length && Input.GetMouseButtonDown(0) && tutorialOn == true)
		{
			TransistionToEndTutorial();
		}
		else if (tutorialOn == false && Input.GetMouseButtonDown(0))
		{
			EndTutorial();
		}
	}
	void ShowNextInteractable()
	{ // Shows each interactable UI one by one during Tutorial
		if (clickCount == 0)
		{ // Initialize first interactable
			UI[clickCount].transform.SetSiblingIndex(infoPanel.transform.GetSiblingIndex() + 1);
		}
		else
		{
			if (clickCount == 2)
			{
				hideDupeCompletionBar();
			}
			UI[clickCount - 1].transform.SetSiblingIndex(infoPanel.transform.GetSiblingIndex() - 1);
			UI[clickCount].transform.SetSiblingIndex(infoPanel.transform.GetSiblingIndex() + 1);
		}
		infoTitle.text = tutorialTitle[clickCount];
		infoText.text = tutorialText[clickCount];
		clickCount++;
	}

	void hideDupeCompletionBar()
	{ // Hide Dupe Completion Bar
		dupeCompletionBar.SetActive(false);
	}

	void TransistionToEndTutorial()
	{ // Shows Cursor and End tutorial panels
		if (clickCount == 7)
		{ // Cursor panel
			UI[UI.Length - 1].transform.SetSiblingIndex(infoPanel.transform.GetSiblingIndex() - 1);
			infoTitle.text = tutorialTitle[clickCount];
			infoText.text = tutorialText[clickCount];
			clickCount++;
		}
		else
		{
			infoTitle.text = tutorialTitle[tutorialTitle.Length - 1];
			infoText.text = tutorialText[tutorialTitle.Length - 1];
			tutorialOn = false;
			cursorManager.ToggleMenuCursor(false);
		}
	}

	void EndTutorial()
	{
		infoPanel.SetActive(false);
		Time.timeScale = 1f;
		tutorialOn = null;
		cursorManager.ToggleMenuCursor(false);
	}

	public void UpdateDaysCounter()
	{
		int seconds = Mathf.FloorToInt(days);
		daysCounterTime.text = days.ToString("0000");
	}

	public void EndGame()
	{
		SceneManager.LoadScene("TitleScreen");
		cursorManager.ToggleMenuCursor(true);
	}

	public void PauseGameUpgrade()
	{
		upgradesMenu.SetActive(true);
		Time.timeScale = 0f;
		cursorManager.ToggleMenuCursor(true);
	}

	public void ResumeGameUpgrade()
	{
		upgradesMenu.SetActive(false);
		Time.timeScale = 1f;
		cursorManager.ToggleMenuCursor(false);
	}

	public void PauseGameResearch()
	{
		researchMenu.SetActive(true);
		Time.timeScale = 0f;
		cursorManager.ToggleMenuCursor(true);
	}

	public void ResumeGameResearch()
	{
		researchMenu.SetActive(false);
		Time.timeScale = 1f;
		cursorManager.ToggleMenuCursor(false);
	}

	public void setCanvas()
	{
		solarSystemButton.SetActive(!viewSolarSystem);
		upgradesButton.SetActive(!viewSolarSystem);
		researchButton.SetActive(!viewSolarSystem);
		completionBar.SetActive(!viewSolarSystem);
		daysCounter.SetActive(!viewSolarSystem);
		missionsDropdown.SetActive(!viewSolarSystem);
		settingsButton.SetActive(!viewSolarSystem);
		backButton.SetActive(viewSolarSystem);
		cursorManager.ToggleMenuCursor(viewSolarSystem);
	}

	IEnumerator switchToSolarSystemView()
	{
		overlayFade.gameObject.SetActive(true);
		yield return StartCoroutine(Fade(1)); // Fade Out

		viewSolarSystem = true;
		playerCamera.enabled = false;
		solarSystemCamera.enabled = true;

		setCanvas();

		yield return StartCoroutine(Fade(0)); // Fade screen in
		overlayFade.gameObject.SetActive(false);
		StartCoroutine(TrackSolarSystemViewTime());
	}

	IEnumerator TrackSolarSystemViewTime()
	{
		while (viewSolarSystem)
		{
			solarSystemTimeViewed += Time.deltaTime;

			if (!hasTriggeredSolarReaction && solarSystemTimeViewed >= 10f)
			{
				hasTriggeredSolarReaction = true;
				OnSolarSystemViewedLongEnough();
			}

			yield return null;
		}
	}

	private void OnSolarSystemViewedLongEnough()
	{
		Debug.Log("You've spent over 10 seconds in the Solar System view!");
		paperLock.UnlockPaper("Orbit & Rotation");
		popUpManager.CreatePopUp("Research Paper #3 Orbit & Rotation is Unlocked");

	}

	private void CheckDayMilestones()
	{
		if (!triggered1400 && days <= 1400)
		{
			triggered1400 = true;
			Debug.Log("Day 1400 milestone reached!");
			paperLock.UnlockPaper("Psyche History");
			popUpManager.CreatePopUp("Research Paper #6 is Unlocked");


			if (!triggered400 && days <= 400)
			{
				triggered400 = true;
				Debug.Log("Day 400 milestone reached!");
				paperLock.UnlockPaper("Psyche Mission Timeline");
				popUpManager.CreatePopUp("Research Paper #7 is Unlocked");

			}
		}
	}

	IEnumerator switchToPsycheWorld()
	{
		overlayFade.gameObject.SetActive(true);
		yield return StartCoroutine(Fade(1)); // Fade Out

		IEnumerator switchToPsycheWorld()
		{
			overlayFade.gameObject.SetActive(true);
			yield return StartCoroutine(Fade(1)); // Fade Out

			viewSolarSystem = false;
			solarSystemCamera.enabled = false;
			playerCamera.enabled = true;

			setCanvas();

			yield return StartCoroutine(Fade(0)); // Fade In
			overlayFade.gameObject.SetActive(false);
		}

	public void showSolarSystemView()
	{
		StartCoroutine(switchToSolarSystemView());
	}

	public void showPsycheWorldView()
	{
		StartCoroutine(switchToPsycheWorld());
	}

	IEnumerator Fade(float targetAlpha)
	{
		float startAlpha = overlayFade.color.a;
		float elapsedTime = 0f;
		Color currentColor = overlayFade.color;
		while (elapsedTime < fadeSpeed)
		{
			currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeSpeed);
			overlayFade.color = currentColor;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		currentColor.a = targetAlpha;
		overlayFade.color = currentColor;
	}

	void checkDayDialogue()
	{
		if (Mathf.FloorToInt(days) == 1200 && !Day1200Flag)
		{
			Day1200Dialogue.Invoke();
			Day1200Flag = true;
		}
		if (Mathf.FloorToInt(days) == 400 && !Day400Flag)
		{
			Day400Dialogue.Invoke();
			Day400Flag = true;
		}
	}

	void checkSolarDialogue()
	{
		if (viewSolarSystem)
		{
			solarSystemSecondsViewed += Time.deltaTime;
			if (Mathf.FloorToInt(solarSystemSecondsViewed) >= 10 && !SolarFlag)
			{
				SolarFlag = true;
				SolarDialogue.Invoke();
			}
		}
		else
		{
			solarSystemSecondsViewed = 0f;
		}
	}
}
}
