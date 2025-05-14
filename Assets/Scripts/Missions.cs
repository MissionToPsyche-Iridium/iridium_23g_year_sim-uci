using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Missions : MonoBehaviour {
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

    public bool task1Transitioned = false;
    public bool task2Transitioned = false;
    public bool task3Transitioned = false;
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

    public bool[] allFlags => new bool[] { magnesiumFlag, ironFlag, nickelFlag, drillReinforcedMagFlag, drillIronFlag, drillNickelFlag,
                                    miningSpeed8Flag, miningSpeed5Flag, miningSpeed2Flag, multiplier2Flag, multiplier5Flag, multiplier10Flag,
                                    flashlight2Flag, flashlight3Flag, missionComplete };

    void Start() {
    }

    void Update() {
        checkStates();
        
        if (expandButton.activeSelf) {
            displayMissions();
            hideMissions();
        }
        else if (collapseButton.activeSelf) {
            displayMissions();
        }
        finalMission();
        setProgress();
    }

    public void displayMissions() {
        mineralMissions();
        drillMissions();
        miningSpeedMissions();
        multiplierMissions();
        flashlightMissions();
    }

    public void hideMissions() {
        Transform topMission = GetFirstActiveChild();
    
        if (topMission == null) {
            return;
        }
        int index = topMission.transform.GetSiblingIndex() + 1;
        for (; index < missionsBody.transform.childCount; index++) {
            missionsBody.transform.GetChild(index).gameObject.SetActive(false);
        }
    }

    Transform GetFirstActiveChild() {
        for (int i = 1; i < missionsBody.transform.childCount; i++) {
            if (missionsBody.transform.GetChild(i).gameObject.activeSelf) {
                return missionsBody.transform.GetChild(i);
            }
        }
        return null;
    }

    public void checkStates() {
        checkMineralState();
        checkDrillState();
        checkMiningSpeedState();
        checkMultiplierState();
        checkFlashlightState();
    }

    public void checkMineralState() {
        if (!magnesiumFlag && upgrades.magnesiumAmount > 0) {
            magnesiumFlag = true;
            MagnesiumEvent.Invoke();
            StartCoroutine(TransitionMineralTask(task1image, task1text, task1CanvasGroup));
        } 
        else if (!ironFlag && upgrades.ironAmount > 0) {
            ironFlag = true;
            IronEvent.Invoke();
            StartCoroutine(TransitionMineralTask(task1image, task1text, task1CanvasGroup));

        } 
        else if (!nickelFlag && upgrades.nickelAmount > 0) {
            nickelFlag = true;
            StartCoroutine(TransitionMineralTask(task1image, task1text, task1CanvasGroup));
        } 
    }

    public void mineralMissions() {
        if (!magnesiumFlag) {
            task1text.text = "Mine Magnesium";
        }
        else if (!ironFlag && drillReinforcedMagFlag) {
            task1.gameObject.SetActive(true);
            StartCoroutine(FadeMineralIn("Mine Iron"));
        }
        else if (!nickelFlag && drillIronFlag ) {
            task1.gameObject.SetActive(true);
            StartCoroutine(FadeMineralIn("Mine Nickel"));
        }
    }
    
    IEnumerator FadeMineralOut() {
        yield return FadeCanvasGroup(task1CanvasGroup, 1f, 0f, 0.5f);
    }

    IEnumerator FadeMineralIn(string newText) {
        task1text.text = newText;
        yield return FadeCanvasGroup(task1CanvasGroup, 0f, 1f, 0.5f);
    }

    public void checkDrillState() {
        if (upgrades.currentDrill == "Reinforced Magnesium") {
            drillReinforcedMagFlag = true;

        } 
        else if (upgrades.currentDrill == "Iron") {
            drillIronFlag = true;
        }
        else if (upgrades.currentDrill == "Nickel") {
            if (!drillNickelFlag) {
                drillNickelFlag = true;
                UpgradeNickelEvent.Invoke();
            }
        }
    }

    public void drillMissions() {
        if (magnesiumFlag && !drillReinforcedMagFlag) {
            task2.gameObject.SetActive(true);
            if (!task2Transitioned) {
                StartCoroutine(FadeCanvasGroup(task2CanvasGroup, 0f, 1f, 0.5f));
                task2Transitioned = true;
            }
        }
        else if (drillReinforcedMagFlag && !drillIronFlag) {
            task2.gameObject.SetActive(true);
            if (!task2Transitioned) {
                StartCoroutine(TransitionTask(task2image, task2text, task2CanvasGroup, "Upgrade Drill to Iron"));
                task2Transitioned = true;
            }
        }
        else if (drillIronFlag && !drillNickelFlag) {
            task2.gameObject.SetActive(true);
            if (!task2Transitioned) {
                StartCoroutine(TransitionTask(task2image, task2text, task2CanvasGroup, "Upgrade Drill to Nickel"));
                task2Transitioned = true;
            }
        }
        else {
            if (drillNickelFlag && !task2Transitioned) {
                StartCoroutine(FadeCanvasGroup(task2CanvasGroup, 1f, 0f, 0.5f));
                task2.gameObject.SetActive(false);
                task2Transitioned = true;
            }
        }
    }

    public void checkMiningSpeedState() {
        if (upgrades.currentMiningSpeed == 8) {
            miningSpeed8Flag = true;
        } 
        else if (upgrades.currentMiningSpeed == 5) {
            miningSpeed5Flag = true;
        }
        else if (upgrades.currentMiningSpeed == 2) {
            if (!miningSpeed2Flag) {
                miningSpeed2Flag = true;
                MiningSpeed2Event.Invoke();
            }
        }
    }

    public void miningSpeedMissions() {
        if (magnesiumFlag && !miningSpeed8Flag) {
            task3.gameObject.SetActive(true);
            if (!task3Transitioned) {
                StartCoroutine(FadeCanvasGroup(task3CanvasGroup, 0f, 1f, 0.5f));
                task3Transitioned = true;
            }
        }
        else if (miningSpeed8Flag && !miningSpeed5Flag) {
            task3.gameObject.SetActive(true);
            if (!task3Transitioned) {
                StartCoroutine(TransitionTask(task3image, task3text, task3CanvasGroup, "Upgrade Mining Speed to 5 seconds"));
                task3Transitioned = true;
            }
        }
        else if (miningSpeed5Flag && !miningSpeed2Flag) {
            task3.gameObject.SetActive(true);
            if (!task3Transitioned) {
                StartCoroutine(TransitionTask(task3image, task3text, task3CanvasGroup, "Upgrade Mining Speed to 2 seconds"));
                task3Transitioned = true;
            }
        }
        else {
            if (miningSpeed2Flag) {
                StartCoroutine(FadeCanvasGroup(task3CanvasGroup, 1f, 0f, 0.5f));
                task3.gameObject.SetActive(false);
                task3Transitioned = false;
            }
        }
    }

    public void checkMultiplierState() {
        if (upgrades.currentResourceMultiplier == 2) {
            multiplier2Flag = true;
        } 
        else if (upgrades.currentResourceMultiplier == 5) {
            multiplier5Flag = true;
        }
        else if (upgrades.currentResourceMultiplier == 10) {
            multiplier10Flag = true;
        }
    }

    public void multiplierMissions() {
        if (magnesiumFlag && !multiplier2Flag) {
            task4.gameObject.SetActive(true);
            if (!task4Transitioned) {
                StartCoroutine(FadeCanvasGroup(task4CanvasGroup, 0f, 1f, 0.5f));
                task4Transitioned = true;
            }
        }
        else if (multiplier2Flag && !multiplier5Flag) {
            task4.gameObject.SetActive(true);
            if (!task4Transitioned) {
                StartCoroutine(TransitionTask(task4image, task4text, task4CanvasGroup, "Upgrade Resource Multiplier to x5"));
                task4Transitioned = true;
            }
        }
        else if (multiplier5Flag && !multiplier10Flag) {
            task4.gameObject.SetActive(true);
            if (!task4Transitioned) {
                StartCoroutine(TransitionTask(task4image, task4text, task4CanvasGroup, "Upgrade Resource Multiplier to x10"));
                task4Transitioned = true;
            }
        }
        else {
            if (multiplier10Flag) {
                StartCoroutine(FadeCanvasGroup(task4CanvasGroup, 1f, 0f, 0.5f));
                task4.gameObject.SetActive(false);
                task4Transitioned = false;
            }
        }
    }

    public void checkFlashlightState() {
        if (upgrades.currentLightStrength == 2) {
            flashlight2Flag = true;
        } 
        else if (upgrades.currentLightStrength == 3) {
            if (!flashlight3Flag) {
                flashlight3Flag = true;
                Flashlight3Event.Invoke();
            }
        }
    }

    public void flashlightMissions() {
        if (ironFlag && !flashlight2Flag) {
            task5.gameObject.SetActive(true);
            if (!task5Transitioned) {
                StartCoroutine(FadeCanvasGroup(task5CanvasGroup, 0f, 1f, 0.5f));
                task5Transitioned = true;
            }
        }
        else if (flashlight2Flag && !flashlight3Flag) {
            task5.gameObject.SetActive(true);
            if (!task5Transitioned) {
                StartCoroutine(TransitionTask(task5image, task5text, task5CanvasGroup, "Upgrade Flashlight to Level 3"));
                task5Transitioned = true;
            }
        }
        else {
            if (flashlight3Flag) {
                StartCoroutine(FadeCanvasGroup(task5CanvasGroup, 1f, 0f, 0.5f));
                task5.gameObject.SetActive(false);
                task5Transitioned = false;
            }
        }
    }

    public bool checkIfAllMissionsComplete() {
        return nickelFlag &&
            drillNickelFlag &&
            miningSpeed2Flag &&
            multiplier10Flag &&
            flashlight3Flag;
    }

    public void finalMission() {
        bool allCompleted = checkIfAllMissionsComplete();
        if (allCompleted && !missionComplete) {
            task1.gameObject.SetActive(true);
            task1text.text = "Report to Mission Control on Mission Completion";
        }
    }

    public void setProgress() {
        float trueCount = 0f;
        for (int i = 0; i < allFlags.Length; i++) {
            if (allFlags[i]) {
                trueCount += 1;
            }
        }
        completionBarImage.fillAmount = trueCount * 0.06666666666666667f; // Change to 0.04 once Research papers are implemented
        percentage.text = (completionBarImage.fillAmount * 100).ToString() + "%";
    }

    IEnumerator TransitionMineralTask(Image checkboxImage, TMP_Text taskText, CanvasGroup canvasGroup) {
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
        task1image.sprite = emptyCheckbox;
        task1.gameObject.SetActive(false);
    }

    IEnumerator TransitionTask(Image checkboxImage, TMP_Text taskText, CanvasGroup canvasGroup, string newText) {
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

    IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration) {
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