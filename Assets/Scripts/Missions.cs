using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class Missions : MonoBehaviour {
    [SerializeField]
    private Image completionBarImage;
    public TMP_Text percentage;

    public GameObject missionsBody;
    public GameObject expandButton;
    public GameObject collapseButton;

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

    public void mineralMissions() {
        if (!magnesiumFlag) {
            task1text.text = "Mine Magnesium";
        }
        else if (!ironFlag && drillReinforcedMagFlag) {
            task1.gameObject.SetActive(true);
            task1text.text = "Mine Iron";
        }
        else if (!nickelFlag && drillIronFlag ) {
            task1.gameObject.SetActive(true);
            task1text.text = "Mine Nickel";
        }
        else {
            task1.gameObject.SetActive(false);
        }
    }

    public void drillMissions() {
        if (magnesiumFlag && !drillReinforcedMagFlag) {
            task2.gameObject.SetActive(true);
            task2text.text = "Upgrade Drill to Reinforced Magnesium";
        }
        else if (drillReinforcedMagFlag && !drillIronFlag) {
            task2.gameObject.SetActive(true);
            task2text.text = "Upgrade Drill to Iron";
        }
        else if (drillIronFlag && nickelFlag && !drillNickelFlag) {
            task2.gameObject.SetActive(true);
            task2text.text = "Upgrade Drill to Nickel";
        }
        else {
            task2.gameObject.SetActive(false);
        }
    }

    public void miningSpeedMissions() {
        if (magnesiumFlag && !miningSpeed8Flag) {
            task3.gameObject.SetActive(true);
            task3text.text = "Upgrade Mining Speed to 8 seconds";
        }
        else if (miningSpeed8Flag && !miningSpeed5Flag) {
            task3.gameObject.SetActive(true);
            task3text.text = "Upgrade Mining Speed to 5 seconds";
        }
        else if (miningSpeed5Flag && nickelFlag && !miningSpeed2Flag) {
            task3.gameObject.SetActive(true);
            task3text.text = "Upgrade Mining Speed to 2 seconds";
        }
        else {
            task3.gameObject.SetActive(false);
        }
    }

    public void multiplierMissions() {
        if (magnesiumFlag && !multiplier2Flag) {
            task4.gameObject.SetActive(true);
            task4text.text = "Upgrade Resource Multiplier to x2";
        }
        else if (multiplier2Flag && !multiplier5Flag) {
            task4.gameObject.SetActive(true);
            task4text.text = "Upgrade Resource Multiplier to x5";
        }
        else if (multiplier5Flag && nickelFlag && !multiplier10Flag) {
            task4.gameObject.SetActive(true);
            task4text.text = "Upgrade Resource Multiplier to x10";
        }
        else {
            task4.gameObject.SetActive(false);
        }
    }

    public void flashlightMissions() {
        if (ironFlag && !flashlight2Flag) {
            task5.gameObject.SetActive(true);
            task5text.text = "Upgrade Flashlight to Level 2";
        }
        else if (flashlight2Flag && nickelFlag && !flashlight3Flag) {
            task5.gameObject.SetActive(true);
            task5text.text = "Upgrade Flashlight to Level 3";
        }
        else {
            task5.gameObject.SetActive(false);
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
}