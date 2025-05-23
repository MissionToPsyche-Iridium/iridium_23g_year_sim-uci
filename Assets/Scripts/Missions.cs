using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


public class Missions : MonoBehaviour
{
    public UnityEvent MagnesiumEvent;
    public UnityEvent IronEvent;
    public UnityEvent UpgradeNickelEvent;
    public UnityEvent MiningSpeed2Event;
    public UnityEvent Flashlight3Event;
    [SerializeField]
    private Image completionBarImage;
    public TMP_Text percentage;

    public GameObject missionsBody;
    public GameObject expandButton;
    public GameObject collapseButton;

    public UpgradesCarousel upgrades;

    public GameObject task1;
    public GameObject task2;
    public GameObject task3;
    public GameObject task4;
    public GameObject task5;

    public TMP_Text task1text;
    public TMP_Text task2text;
    public TMP_Text task3text;
    public TMP_Text task4text;
    public TMP_Text task5text;

    public Image task1image;
    public Image task2image;
    public Image task3image;
    public Image task4image;
    public Image task5image;
    public Sprite emptyCheckbox;
    public Sprite crossedCheckbox;

    public CanvasGroup task1CanvasGroup;
    public CanvasGroup task2CanvasGroup;
    public CanvasGroup task3CanvasGroup;
    public CanvasGroup task4CanvasGroup;
    public CanvasGroup task5CanvasGroup;

    public bool task1Transitioned = false; // Prevents Update from rapidly switching between unchecked and checked checkbox images 
    public bool task2Transitioned = false; // and glitching mission animtion. If false, it begins the mission animation and transitions
    public bool task3Transitioned = false; // to true. Else, it prevents the animation from playing.
    public bool task4Transitioned = false;
    public bool task5Transitioned = false;

    public bool magnesiumFlag = false;
    public bool ironFlag = false;
    public bool nickelFlag = false;
    public bool drillReinforcedMagFlag = false;
    public bool drillIronFlag = false;
    public bool drillNickelFlag = false;
    public bool miningSpeed8Flag = false;
    public bool miningSpeed5Flag = false;
    public bool miningSpeed2Flag = false;
    public bool multiplier2Flag = false;
    public bool multiplier5Flag = false;
    public bool multiplier10Flag = false;
    public bool flashlight2Flag = false;
    public bool flashlight3Flag = false;
    public bool missionComplete = false;
    public ResearchPaperLock paperLock;
    public PopUpManager popUpManager;

    public bool[] allFlags => new bool[] { magnesiumFlag, ironFlag, nickelFlag, drillReinforcedMagFlag, drillIronFlag, drillNickelFlag,
                                    miningSpeed8Flag, miningSpeed5Flag, miningSpeed2Flag, multiplier2Flag, multiplier5Flag, multiplier10Flag,
                                    flashlight2Flag, flashlight3Flag, missionComplete };

    void Start() {
		SoundManager.StopSound(SoundType.MENU_THEME); // Stop menu music
		SoundManager.LoopSound(SoundType.GAME_AMBIENCE); // Begin game ambience upon Mission start
    }

    void Update()
    {
        checkStates();

        if (expandButton.activeSelf)
        {
            displayMissions();
            hideMissions();
        }
        else if (collapseButton.activeSelf)
        {
            displayMissions();
        }
        finalMission();
        setProgress();
    }

    public void displayMissions() // How a mission will be displayed if active or not
    {
        mineralMissions();
        drillMissions();
        miningSpeedMissions();
        multiplierMissions();
        flashlightMissions();
    }

    public void hideMissions() // Hids any everything but one mission for display when in collapse mode
    {
        Transform topMission = GetFirstActiveChild();

        if (topMission == null)
        {
            return;
        }
        int index = topMission.transform.GetSiblingIndex() + 1;
        for (; index < missionsBody.transform.childCount; index++)
        {
            missionsBody.transform.GetChild(index).gameObject.SetActive(false);
        }
    }

    Transform GetFirstActiveChild() // Gets the first active mission
    {
        for (int i = 1; i < missionsBody.transform.childCount; i++)
        {
            if (missionsBody.transform.GetChild(i).gameObject.activeSelf)
            {
                return missionsBody.transform.GetChild(i);
            }
        }
        return null;
    }

    public void checkStates() // Checks the states of all mission types
    {
        checkMineralState();
        checkDrillState();
        checkMiningSpeedState();
        checkMultiplierState();
        checkFlashlightState();
    }

    public void checkMineralState() // Checks if a mineral has been mined, updates the flag to true for that mineral
    {
        if (!magnesiumFlag && upgrades.magnesiumAmount > 0)
        {
            magnesiumFlag = true;
            paperLock.UnlockPaper("Core");
            popUpManager.CreatePopUp("Research Paper #1 is Unlocked");
            MagnesiumEvent.Invoke();
            StartCoroutine(FinishTaskTransition(task1, task1image, task1text, task1CanvasGroup));
        }
        else if (!ironFlag && upgrades.ironAmount > 0)
        {
            ironFlag = true;
            paperLock.UnlockPaper("Metals");
            popUpManager.CreatePopUp("Research Paper #2 is Unlocked");
            IronEvent.Invoke();
            StartCoroutine(FinishTaskTransition(task1, task1image, task1text, task1CanvasGroup));

        }
        else if (!nickelFlag && upgrades.nickelAmount > 0)
        {
            nickelFlag = true;
            StartCoroutine(FinishTaskTransition(task1, task1image, task1text, task1CanvasGroup));
        }
    }

    public void mineralMissions() // How mineral missions will be displayed
    {
        if (!magnesiumFlag)
        {
            task1text.text = "Mine Magnesium";
        }
        else if (!ironFlag && drillReinforcedMagFlag)
        {
            task1.gameObject.SetActive(true);
            StartCoroutine(FadeTaskIn("Mine Iron"));
        }
        else if (!nickelFlag && drillIronFlag)
        {
            task1.gameObject.SetActive(true);
            StartCoroutine(FadeTaskIn("Mine Nickel"));
        }
    }

    IEnumerator FadeTaskIn(string newText) // Solely for mineral missions & final mission to fade in
    {
        task1text.text = newText;
        yield return FadeCanvasGroup(task1CanvasGroup, 0f, 1f, 0.5f);
    }

    public void checkDrillState() // Checks for drill state from Upgrades, updates respective drill flag to true
    {
        if (upgrades.currentDrill == "Reinforced Magnesium")
        {
            drillReinforcedMagFlag = true;

        }
        else if (upgrades.currentDrill == "Iron")
        {
            drillIronFlag = true;
        }
        else if (upgrades.currentDrill == "Nickel")
        {
            if (!drillNickelFlag)
            {
                drillNickelFlag = true;
                UpgradeNickelEvent.Invoke();
            }
        }
    }

    public void drillMissions() // Checks flags and displays the appropriate task for drill missions. Starts task animation.
    {
        if (magnesiumFlag && !drillReinforcedMagFlag) // Checks flag
        {
            task2.gameObject.SetActive(true);
            if (!task2Transitioned) // Checks if task has been animated yet
            {
                StartCoroutine(FadeCanvasGroup(task2CanvasGroup, 0f, 1f, 0.5f)); // Task fades in
                task2Transitioned = true; // Task has completed its animation
            }
        }
        else if (drillReinforcedMagFlag && !drillIronFlag)
        {
            task2.gameObject.SetActive(true);
            if (!task2Transitioned)
            {
                StartCoroutine(TransitionTask(task2image, task2text, task2CanvasGroup, "Upgrade Drill to Iron"));
                task2Transitioned = true;
            }
        }
        else if (drillIronFlag && !drillNickelFlag)
        {
            task2.gameObject.SetActive(true);
            if (!task2Transitioned)
            {
                StartCoroutine(TransitionTask(task2image, task2text, task2CanvasGroup, "Upgrade Drill to Nickel"));
                task2Transitioned = true;
            }
        }
        else
        {
            if (drillNickelFlag && !task2Transitioned) // Removes drill task object and begins finished task animation
            {
                StartCoroutine(FinishTaskTransition(task2, task2image, task2text, task2CanvasGroup));
                task2Transitioned = true;
            }
        }
    }

    public void checkMiningSpeedState() // Checks for mining speed state from Upgrades, updates respective mining speed flag to true
    {
        if (upgrades.currentMiningSpeed == 8)
        {
            miningSpeed8Flag = true;
        }
        else if (upgrades.currentMiningSpeed == 5)
        {
            miningSpeed5Flag = true;
        }
        else if (upgrades.currentMiningSpeed == 2)
        {
            if (!miningSpeed2Flag)
            {
                miningSpeed2Flag = true;
                MiningSpeed2Event.Invoke();
            }
        }
    }

    public void miningSpeedMissions() // Checks flags and displays the appropriate task for mining speed missions. Starts task animation.
    {
        if (magnesiumFlag && !miningSpeed8Flag)
        {
            task3.gameObject.SetActive(true);
            if (!task3Transitioned)
            {
                StartCoroutine(FadeCanvasGroup(task3CanvasGroup, 0f, 1f, 0.5f));
                task3Transitioned = true;
            }
        }
        else if (miningSpeed8Flag && !miningSpeed5Flag)
        {
            task3.gameObject.SetActive(true);
            if (!task3Transitioned)
            {
                StartCoroutine(TransitionTask(task3image, task3text, task3CanvasGroup, "Upgrade Mining Speed to 5 seconds"));
                task3Transitioned = true;
            }
        }
        else if (miningSpeed5Flag && !miningSpeed2Flag)
        {
            task3.gameObject.SetActive(true);
            if (!task3Transitioned)
            {
                StartCoroutine(TransitionTask(task3image, task3text, task3CanvasGroup, "Upgrade Mining Speed to 2 seconds"));
                task3Transitioned = true;
            }
        }
        else
        {
            if (miningSpeed2Flag)
            {
                StartCoroutine(FinishTaskTransition(task3, task3image, task3text, task3CanvasGroup));
                task3Transitioned = false;
            }
        }
    }

    public void checkMultiplierState() // Checks for multiplier state from Upgrades, updates respective multiplier flag to true
    {
        if (upgrades.currentResourceMultiplier == 2)
        {
            multiplier2Flag = true;
        }
        else if (upgrades.currentResourceMultiplier == 5)
        {
            multiplier5Flag = true;
        }
        else if (upgrades.currentResourceMultiplier == 10)
        {
            multiplier10Flag = true;
        }
    }

    public void multiplierMissions() // Checks flags and displays the appropriate task for multiplier missions. Starts task animation.
    {
        if (magnesiumFlag && !multiplier2Flag)
        {
            task4.gameObject.SetActive(true);
            if (!task4Transitioned)
            {
                StartCoroutine(FadeCanvasGroup(task4CanvasGroup, 0f, 1f, 0.5f));
                task4Transitioned = true;
            }
        }
        else if (multiplier2Flag && !multiplier5Flag)
        {
            task4.gameObject.SetActive(true);
            if (!task4Transitioned)
            {
                StartCoroutine(TransitionTask(task4image, task4text, task4CanvasGroup, "Upgrade Resource Multiplier to x5"));
                task4Transitioned = true;
            }
        }
        else if (multiplier5Flag && !multiplier10Flag)
        {
            task4.gameObject.SetActive(true);
            if (!task4Transitioned)
            {
                StartCoroutine(TransitionTask(task4image, task4text, task4CanvasGroup, "Upgrade Resource Multiplier to x10"));
                task4Transitioned = true;
            }
        }
        else
        {
            if (multiplier10Flag)
            {
                StartCoroutine(FinishTaskTransition(task4, task4image, task4text, task4CanvasGroup));
                task4Transitioned = false;
            }
        }
    }

    public void checkFlashlightState()
    {
        if (upgrades.currentLightStrength == 2)
        {
            flashlight2Flag = true;
        }
        else if (upgrades.currentLightStrength == 3)
        {
            if (!flashlight3Flag)
            {
                flashlight3Flag = true;
                Flashlight3Event.Invoke();
            }
        }
    }

    public void flashlightMissions()
    {
        if (ironFlag && !flashlight2Flag)
        {
            task5.gameObject.SetActive(true);
            if (!task5Transitioned)
            {
                StartCoroutine(FadeCanvasGroup(task5CanvasGroup, 0f, 1f, 0.5f));
                task5Transitioned = true;
            }
        }
        else if (flashlight2Flag && !flashlight3Flag)
        {
            task5.gameObject.SetActive(true);
            if (!task5Transitioned)
            {
                StartCoroutine(TransitionTask(task5image, task5text, task5CanvasGroup, "Upgrade Flashlight to Level 3"));
                task5Transitioned = true;
            }
        }
        else
        {
            if (flashlight3Flag)
            {
                StartCoroutine(FinishTaskTransition(task5, task5image, task5text, task5CanvasGroup));
                task5Transitioned = false;
            }
        }
    }

    public bool checkIfAllMissionsComplete() // Checks if all upgrades are mxed out
    {
        return nickelFlag &&
            drillNickelFlag &&
            miningSpeed2Flag &&
            multiplier10Flag &&
            flashlight3Flag;
    }

    public void finalMission() // Initializes the final mission
    {
        bool allCompleted = checkIfAllMissionsComplete();
        if (allCompleted && !missionComplete)
        {
            task1.gameObject.SetActive(true);
            StartCoroutine(FadeTaskIn("Report to Mission Control on Mission Completion"));
        }
    }

    public void setProgress() // Sets the progress visual for the progress bar
    {
        float trueCount = 0f;
        for (int i = 0; i < allFlags.Length; i++)
        {
            if (allFlags[i])
            {
                trueCount += 1;
            }
        }
        float totalFlags = allFlags.Length;
        float percentageValue = (trueCount / totalFlags) * 100f;

        completionBarImage.fillAmount = percentageValue / 100f;
        percentage.text = percentageValue.ToString("F2") + "%";
    }

    IEnumerator FinishTaskTransition(GameObject task, Image checkboxImage, TMP_Text taskText, CanvasGroup canvasGroup) // Final task animation
    {
        // 1. Fade out old content
        yield return FadeCanvasGroup(canvasGroup, 1f, 0f, 0.5f);

        // 2. Show checked sprite and fade in
        checkboxImage.sprite = crossedCheckbox;
        yield return FadeCanvasGroup(canvasGroup, 0f, 1f, 0.5f);

        // 3. Wait
        yield return new WaitForSeconds(2f);

        // 4. Fade out checked content
        yield return FadeCanvasGroup(canvasGroup, 1f, 0f, 0.5f);

        // 5. Revert to empty and hide
        checkboxImage.sprite = emptyCheckbox;
        task.gameObject.SetActive(false);
    }

    IEnumerator TransitionTask(Image checkboxImage, TMP_Text taskText, CanvasGroup canvasGroup, string newText) // Transition to next task animation
    {
        // 1. Fade out old content
        yield return FadeCanvasGroup(canvasGroup, 1f, 0f, 0.5f);

        // 2. Show checked sprite and fade in
        checkboxImage.sprite = crossedCheckbox;
        yield return FadeCanvasGroup(canvasGroup, 0f, 1f, 0.5f);

        // 3. Wait
        yield return new WaitForSeconds(2f);

        // 4. Fade out checked content
        yield return FadeCanvasGroup(canvasGroup, 1f, 0f, 0.5f);

        // 5. Update to new task
        taskText.text = newText;
        checkboxImage.sprite = emptyCheckbox;

        // 6. Fade in new content
        yield return FadeCanvasGroup(canvasGroup, 0f, 1f, 0.5f);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration) // Fades the canvas group to target alpha
    {
        float time = 0f;
        while (time < duration)
        {
            group.alpha = Mathf.Lerp(from, to, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        group.alpha = to;
    }
}