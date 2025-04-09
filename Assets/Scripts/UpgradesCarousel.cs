using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UpgradesCarousel : MonoBehaviour {
    public int index = 0;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Image image;
    public TMP_Text type;
    public TMP_Text from;
    public TMP_Text to;
    public TMP_Text mat1;
    public TMP_Text req1;
    public TMP_Text mat2;
    public TMP_Text req2;
    public TMP_Text mat3;
    public TMP_Text req3;
    private List<TMP_Text> matsList;
    private List<TMP_Text> reqsList;

    [SerializeField] public Sprite drillImage;
    [SerializeField] public Sprite miningSpeedImage;
    [SerializeField] public Sprite resourceMultiplierImage;
    [SerializeField] public Sprite flashlightImage;

    [SerializeField] public string currentDrill = "Magnesium";
    [SerializeField] public int currentMiningSpeed = 10;
    [SerializeField] public int currentResourceMultiplier = 1;
    [SerializeField] public int currentLightStrength = 1;
    [SerializeField] public int magnesiumAmount = 0;
    [SerializeField] public int ironAmount = 0;
    [SerializeField] public int nickelAmount = 0;
    private List<int> matAmountsList;
    private Dictionary<int, (string type, Sprite typeImage, object current)> upgradeTypes;
    private Dictionary<string, (string next, List<string> mats, List<int> amountRequired)> drillUpgrades;
    private Dictionary<int, (int next, List<string> mats, List<int> amountRequired)> miningSpeedUpgrades;
    private Dictionary<int, (int next, List<string> mats, List<int> amountRequired)> resourceMultiplierUpgrades;
    private Dictionary<int, (int next, List<string> mats, List<int> amountRequired)> lightStrengthUpgrades;

    void Start() {
        matsList = new List<TMP_Text> { mat1, mat2, mat3 };
        reqsList = new List<TMP_Text> { req1, req2, req3 };

        upgradeTypes = new Dictionary<int, (string, Sprite, object)> {
            { 0, ("Drill", drillImage, currentDrill) },
            { 1, ("Mining Speed", miningSpeedImage, currentMiningSpeed) },
            { 2, ("Resource Multiplier", resourceMultiplierImage, currentResourceMultiplier) },
            { 3, ("Flashlight Strength", flashlightImage, currentLightStrength) }
        };

        drillUpgrades = new Dictionary<string, (string, List<string>, List<int>)> {
            { "Magnesium", ("Reinforced Magnesium", new List<string> { "Magnesium" }, new List<int> { 10 }) },
            { "Reinforced Magnesium", ("Iron", new List<string> { "Magnesium", "Iron" }, new List<int> { 20, 10 }) },
            { "Iron", ("Nickel", new List<string> { "Magnesium", "Iron", "Nickel" }, new List<int> { 50, 30, 10 }) }
        };

        miningSpeedUpgrades = new Dictionary<int, (int next, List<string> mats, List<int> amountRequired)> {
            { 10, (8, new List<string> { "Magnesium" }, new List<int> { 10 }) },
            { 8, (5, new List<string> { "Magnesium ", "Iron" }, new List<int> { 25, 10 }) },
            { 5, (2, new List<string> { "Nickel" }, new List<int> { 10 }) }
        };

        resourceMultiplierUpgrades = new Dictionary<int, (int next, List<string> mats, List<int> amountRequired)> {
            { 1, (2, new List<string> { "Magnesium" }, new List<int> { 20 }) },
            { 2, (5, new List<string> { "Iron" }, new List<int> { 20 }) },
            { 5, (10, new List<string> { "Nickel" }, new List<int> { 20 }) }
        };

        lightStrengthUpgrades = new Dictionary<int, (int next, List<string> mats, List<int> amountRequired)> {
            { 1, (2, new List<string> { "Magnesium" }, new List<int> { 10 }) },
            { 2, (3, new List<string> { "Iron" }, new List<int> { 10 }) }
        };

        matAmountsList = new List<int> { magnesiumAmount, ironAmount, nickelAmount };

        displayPageInformation();
    }

    public void Prev() {
        if (index != 0) {
            index -= 1;
        }
        displayPageInformation();
    }

    public void Next() {
        if (index != 3) {
            index += 1;
        }
        displayPageInformation();
    }

    public void displayPageInformation() {
        type.text = upgradeTypes[index].type;
        image.sprite = upgradeTypes[index].typeImage;
        from.text = upgradeTypes[index].current.ToString();

        mat1.gameObject.SetActive(false);
        mat2.gameObject.SetActive(false);
        mat3.gameObject.SetActive(false);
        req1.gameObject.SetActive(false);
        req2.gameObject.SetActive(false);
        req3.gameObject.SetActive(false);
        
        switch (index) {
            case 1:
                from.text = upgradeTypes[index].current.ToString() + " seconds";
                to.text = miningSpeedUpgrades[currentMiningSpeed].next.ToString() + " seconds";
                materialRequiredSetUp(miningSpeedUpgrades[currentMiningSpeed].mats, miningSpeedUpgrades[currentMiningSpeed].amountRequired);
                break;
            case 2:
                from.text = upgradeTypes[index].current.ToString() + " multiplier";
                to.text = resourceMultiplierUpgrades[currentResourceMultiplier].next.ToString() + " multiplier";
                materialRequiredSetUp(resourceMultiplierUpgrades[currentResourceMultiplier].mats, resourceMultiplierUpgrades[currentResourceMultiplier].amountRequired);;
                break;
            case 3:
                from.text = "Level " + upgradeTypes[index].current.ToString();
                to.text = "Level " + lightStrengthUpgrades[currentLightStrength].next.ToString();
                materialRequiredSetUp(lightStrengthUpgrades[currentLightStrength].mats, lightStrengthUpgrades[currentLightStrength].amountRequired);
                break;
            default:
                from.text = upgradeTypes[index].current.ToString();
                to.text = drillUpgrades[currentDrill].next;
                materialRequiredSetUp(drillUpgrades[currentDrill].mats, drillUpgrades[currentDrill].amountRequired);
                break;
        }
    }

    public void materialRequiredSetUp(List<string> mats, List<int> amountRequired) {
        for (int i = 0; i < mats.Count; i++) {
            matsList[i].gameObject.SetActive(true);
            matsList[i].text = mats[i];
            reqsList[i].gameObject.SetActive(true);
            reqsList[i].text =  matAmountsList[i] +  "/" + amountRequired[i].ToString();
        };
    }
}