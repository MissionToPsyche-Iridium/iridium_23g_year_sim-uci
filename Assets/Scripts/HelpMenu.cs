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

    public Sprite cursorImage;
    public Sprite miningImage;
    public Sprite missionsImage;
    public Sprite progressImage;
    public Sprite dayscoundownImage;
    public Sprite solarsystemviewImage;
    public Sprite upgradesImage;
    public Sprite researchImage;
    public Sprite hint1Image;
    public Sprite hint2Image;
    public Sprite hint3Image;
    public Sprite hint4Image;
    public Sprite hint5Image;
    public Sprite hintTimeImage;
    public Sprite hintDrillImage;
    public Sprite hintMiningSpeedImage;


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
        helpText.text = "Holding the Alt key (or Option key) will show the cursor to interact with the on-screen buttons.";
        helpImage.sprite = cursorImage;
    }

    public void MiningButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Mining";
        helpText.text = "Mine ore deposits to gather ores by hovering your cursor over the ore and holding right click.";
    }

    public void MissionsButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Missions";
        helpText.text = "Missions will help guide you to completing your main mission: Gather as much data from Psyche as you can in a year. Complete them all to finish the game.";
    }

    public void ProgressBarButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Progress Bar";
        helpText.text = "This bar shows how much progress you have made to completing your Psyche mission. Completing missions, maxing upgrades, and generating research papers will increase your progress.";
    }

    public void DaysCountdownButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Days Countdown";
        helpText.text = "Psyche has 1828 days in a year. For the sake of gameplay, each day is a second in real time. Complete your mission before time is up. Time stops when Upgrades, Research, or Settings screens are open.";
        helpImage.sprite = dayscoundownImage;
    }

    public void SolarSystemViewButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Solar System View";
        helpText.text = "This is where you can view where Psyche is in the Solar System and keep track of its orbit in the year anytime during your gameplay.";
        helpImage.sprite = solarsystemviewImage;
    }

    public void UpgradesButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Upgrades";
        helpText.text = "Gather minerals and use them to upgrade your Rover throughout the game! Max out upgrades before the game ends to complete your mission.";
        helpImage.sprite = upgradesImage;
    }

    public void ResearchButton() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research";
        helpText.text = "Any research paper you generate can be found in here. Check in here anytime to read up any interesting facts you have found from exploring Psyche!";
        helpImage.sprite = researchImage;
    }

    public void ResearchPaper1Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #1 Hint";
        helpText.text = "Mine a certain ore for the first time...";
    }

    public void ResearchPaper2Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #2 Hint";
        helpText.text = "Mine a certain ore for the first time...";
    }

    public void ResearchPaper3Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #3 Hint";
        helpText.text = "Check out the Solar System View for a little while...";
        helpImage.sprite = hint3Image;
    }

    public void ResearchPaper4Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #4 Hint";
        helpText.text = "Explore around the Southern Hemisphere of Psyche...Watch where you are hovering!";
        helpImage.sprite = hint4Image;
    }

    public void ResearchPaper5Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #5 Hint";
        helpText.text = "Explore around the Southern Hemisphere of Psyche...Watch where you are hovering!";
    }

    public void ResearchPaper6Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #6 Hint";
        helpText.text = "Wait until a certain day has been reached...";
        helpImage.sprite = hintTimeImage;
    }

    public void ResearchPaper7Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #7 Hint";
        helpText.text = "Wait until a certain day has been reached...";
        helpImage.sprite = hintTimeImage;
    }

    public void ResearchPaper8Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #8 Hint";
        helpText.text = "Keep upgrading your drill...";
        helpImage.sprite = hintDrillImage;
    }

    public void ResearchPaper9Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #9 Hint";
        helpText.text = "Keep upgrading your mining speed...";
        helpImage.sprite = hintMiningSpeedImage;
    }

    public void ResearchPaper10Hint() {
        infoContent.gameObject.SetActive(true);
        helpTitle.text = "Research Paper #10 Hint";
        helpText.text = "Keep upgrading your flashlight...";
    }
 }