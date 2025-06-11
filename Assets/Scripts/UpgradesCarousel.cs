using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class UpgradesCarousel : MonoBehaviour {
    public Missions missions;

    private int index = 0; // Tracks which page the user was on
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Image image;
    public TMP_Text type;
    public TMP_Text from;
    public TMP_Text to;
    public TMP_Text error;

    public VerticalLayoutGroup descriptionGroup;
    private Transform maxUpgradeText;
    private Transform toFromGroup;
    private Transform requirementsGroup;

    public TMP_Text mat1; // Material 1 Text
    public TMP_Text req1; // Material Requirement Number Text
    public TMP_Text mat2;
    public TMP_Text req2;
    public TMP_Text mat3;
    public TMP_Text req3;
    private List<TMP_Text> matsList;
    private List<TMP_Text> reqsList;

    [SerializeField] public Sprite drillImage;
    [SerializeField] public Sprite miningSpeedImage;
    [SerializeField] public Sprite resourceMultiplierImage;

    [SerializeField] public string currentDrill = "Magnesium";
    [SerializeField] public int currentMiningSpeed = 10;
    [SerializeField] public int currentResourceMultiplier = 1;
    [SerializeField] public int magnesiumAmount = 0;
    [SerializeField] public int ironAmount = 0;
    [SerializeField] public int nickelAmount = 0;
    private Dictionary<string, int> matAmountsList; // Dictionary of user's mineral amounts. Note: when updating amounts through this dictionary, make sure to call SyncMatsFromDict() after
    private Dictionary<int, (string type, Sprite typeImage, bool isMax)> upgradeTypes;
    private Dictionary<string, (string next, Dictionary<string, int> requirements)> drillUpgrades;
    private Dictionary<int, (int next, Dictionary<string, int> requirements)> miningSpeedUpgrades;
    private Dictionary<int, (int next, Dictionary<string, int> requirements)> resourceMultiplierUpgrades;

    public ResearchPaperLock paperLock;
	public PopUpManager popUpManager;

    void Start() {
        matsList = new List<TMP_Text> { mat1, mat2, mat3 };
        reqsList = new List<TMP_Text> { req1, req2, req3 };

        upgradeTypes = new Dictionary<int, (string, Sprite, bool)> {
            { 0, ("Drill", drillImage, false) },
            { 1, ("Mining Speed", miningSpeedImage, false) },
            { 2, ("Resource Multiplier", resourceMultiplierImage, false) }
        };

        drillUpgrades = new Dictionary<string, (string, Dictionary<string, int>)> {
            { "Magnesium", ("Reinforced Magnesium", new Dictionary<string, int> { // Magnesium -> Reinforced Magnesium
                    { "Magnesium", 1 } 
                }) 
            },
            { "Reinforced Magnesium", ("Iron", new Dictionary<string, int> { // Reinforced Magnesium -> Iron
                    { "Magnesium", 3 }, 
                    { "Iron", 1 }
                })
            },
            { "Iron", ("Nickel", new Dictionary<string, int> { // Iron -> Nickel
                    { "Magnesium", 5 }, 
                    { "Iron", 3 }, 
                    { "Nickel", 1 } 
                })
            }
        };

        miningSpeedUpgrades = new Dictionary<int, (int next, Dictionary<string, int>)> {
            { 10, (8, new Dictionary<string, int> { // 10 seconds -> 8 seconds
                    { "Magnesium", 1 }
                }) 
            },
            { 8, (5, new Dictionary<string, int> { // 8 seconds -> 5 seconds
                    { "Magnesium", 3 },
                    { "Iron", 1 }
                }) 
            },
            { 5, (2, new Dictionary<string, int> { // 5 seconds -> 2 seconds
                    { "Iron", 3 },
                    { "Nickel", 1 }
                }) 
            }
        };

        resourceMultiplierUpgrades = new Dictionary<int, (int next, Dictionary<string, int>)> {
            { 1, (2, new Dictionary<string, int> { // 1x -> 2x
                    { "Magnesium", 1 }
                }) 
            },
            { 2, (5, new Dictionary<string, int> { // 2x -> 5x
                    { "Iron", 1 }
                }) 
            },
            { 5, (10, new Dictionary<string, int> { // 5x -> 10x
                    { "Nickel", 1 }
                }) 
            }
        };

        matAmountsList = new Dictionary<string, int> { 
            { "Magnesium", magnesiumAmount},
            { "Iron", ironAmount},
            { "Nickel", nickelAmount }
        };
        
        maxUpgradeText = descriptionGroup.transform.GetChild(1);
        toFromGroup = descriptionGroup.transform.GetChild(2);
        requirementsGroup = descriptionGroup.transform.GetChild(3);

        displayPageInformation();
    }

    // Raymond - Added this function to test the upgrade system
    public void AddMaterial(string type, int amount) {
        matAmountsList[type] += amount;
        SyncMatsFromDict();
        UpdateResourceUI();
        displayPageInformation();
    }

    public void UpdateResourceUI() {
        mat1.text = "Magnesium: " + matAmountsList["Magnesium"].ToString();
        mat2.text = "Iron: " + matAmountsList["Iron"].ToString();
        mat3.text = "Nickel: " + matAmountsList["Nickel"].ToString();
    }

    void Update() {
        descriptionSetUp();
    }

    public void Prev() {
        if (index != 0)
        {
            index -= 1;
        }
        displayPageInformation();
    }

    public void Next()
    {
        if (index != 2)
        {
            index += 1;
        }
        displayPageInformation();
    }

    public void displayPageInformation() {
        type.text = upgradeTypes[index].type;
        image.sprite = upgradeTypes[index].typeImage;

        resetMaterialsNeededInformation();
        var upgrade = upgradeTypes[index];

        if (upgrade.isMax || checkIfMaxUpgradeReached()) {
            upgrade.isMax = true;
            upgradeTypes[index] = upgrade;
            descriptionGroup.padding.bottom = 0;
        }
        else {
            descriptionGroup.padding.bottom = -85;
            descriptionSetUp();
        }
        displayMaxUpgradeText(upgrade.isMax);
    }

    public void resetMaterialsNeededInformation() {
        mat1.gameObject.SetActive(false);
        mat2.gameObject.SetActive(false);
        mat3.gameObject.SetActive(false);
        req1.gameObject.SetActive(false);
        req2.gameObject.SetActive(false);
        req3.gameObject.SetActive(false);
    }

    public void displayMaxUpgradeText(bool isMax) { // Hides to, from, and requirements descriptions and unhides max upgrade reached text
        toFromGroup.gameObject.SetActive(!isMax);
        requirementsGroup.gameObject.SetActive(!isMax);
        maxUpgradeText.gameObject.gameObject.SetActive(isMax);
    }

    public void descriptionSetUp() { // Sets up & displays to, from, and material required descriptions
        switch (index) {
            case 1:
                if (currentMiningSpeed != 2) {
                    from.text = currentMiningSpeed.ToString() + " seconds";
                    to.text = miningSpeedUpgrades[currentMiningSpeed].next.ToString() + " seconds";
                    materialRequiredSetUp(miningSpeedUpgrades[currentMiningSpeed].requirements);
                }
                break;
            case 2:
                if (currentResourceMultiplier != 10) {
                    from.text = currentResourceMultiplier.ToString() + "x";
                    to.text = resourceMultiplierUpgrades[currentResourceMultiplier].next.ToString() + "x";
                    materialRequiredSetUp(resourceMultiplierUpgrades[currentResourceMultiplier].requirements); ;
                }
                break;
            default:
                if (currentDrill != "Nickel") {
                    from.text = currentDrill;
                    to.text = drillUpgrades[currentDrill].next;
                    materialRequiredSetUp(drillUpgrades[currentDrill].requirements);
                }
                break;
        }
    }

    public void materialRequiredSetUp(Dictionary<string, int> requirements) { // Displays all materials required
        List<string> keys = requirements.Keys.ToList();
        SyncDictFromMats(); // Sync dictionary 

        for (int i = 0; i < keys.Count; i++) {
            matsList[i].gameObject.SetActive(true);
            matsList[i].text = keys[i];
            reqsList[i].gameObject.SetActive(true);
            reqsList[i].text =  matAmountsList[keys[i]] +  "/" + requirements[keys[i]].ToString();
        }
    }

    public bool checkIfMaxUpgradeReached() {
        if (index == 0 && currentDrill == "Nickel") { // Checks if drill upgrade max
            return true;
        }
        else if (index == 1 && currentMiningSpeed == 2) { // Checks if mining speed upgrade max
            return true;
        }
        else if (index == 2 && currentResourceMultiplier == 10) { // Checks if resource multiplier upgrade max
            return true;
        }
        else {
            return false;
        }
    }

    public bool checkIfCanUpgrade(Dictionary<string, int> requirements) { // Checks if user has enough minerals to upgrade
        List<string> keys = requirements.Keys.ToList();

        for (int i = 0; i < keys.Count; i++) {
            if (matAmountsList[keys[i]] < requirements[keys[i]]) {
                return false;
            }
        }
        return true;
    }

    public void upgradeSelectedUpgrade() { // Upgrades the selected upgrade
        if (index == 0 && !checkIfMaxUpgradeReached() && checkIfCanUpgrade(drillUpgrades[currentDrill].requirements))
        {
            deductMineralAmount(drillUpgrades[currentDrill].requirements);
            currentDrill = drillUpgrades[currentDrill].next;
            missions.task2Transitioned = false;

        }
        else if (index == 1 && !checkIfMaxUpgradeReached() && checkIfCanUpgrade(miningSpeedUpgrades[currentMiningSpeed].requirements))
        {
            deductMineralAmount(miningSpeedUpgrades[currentMiningSpeed].requirements);
            currentMiningSpeed = miningSpeedUpgrades[currentMiningSpeed].next;
            missions.task3Transitioned = false;
        }
        else if (index == 2 && !checkIfMaxUpgradeReached() && checkIfCanUpgrade(resourceMultiplierUpgrades[currentResourceMultiplier].requirements))
        {
            deductMineralAmount(resourceMultiplierUpgrades[currentResourceMultiplier].requirements);
            currentResourceMultiplier = resourceMultiplierUpgrades[currentResourceMultiplier].next;
            missions.task4Transitioned = false;
        }
        else if (!checkIfMaxUpgradeReached())
        {
            StartCoroutine(ErrorPopUpTextFade()); // Error text appears when not enough resources
        }
        displayPageInformation(); // Updates page after upgrade
    }

    public void deductMineralAmount(Dictionary<string, int> requirements)
    {  // Player's mineral amount - required mineral amount for upgrade = new Player's mineral amount
        List<string> keys = requirements.Keys.ToList();

        for (int i = 0; i < keys.Count; i++)
        {
            matAmountsList[keys[i]] -= requirements[keys[i]];
            if (matAmountsList[keys[i]] < 0)
            {
                matAmountsList[keys[i]] = 0;
            }
        }
        SyncMatsFromDict();
    }

    public void SyncMatsFromDict() { // matAmountsList only stores copies so need to manually update the variables
        magnesiumAmount = matAmountsList["Magnesium"];
        ironAmount = matAmountsList["Iron"];
        nickelAmount = matAmountsList["Nickel"];
    }

    public void SyncDictFromMats() { // matAmountsList only stores copies so need to manually update the dictionary from the variables
        matAmountsList["Magnesium"] = magnesiumAmount;
        ironAmount = matAmountsList["Iron"] = ironAmount;
        matAmountsList["Nickel"] = nickelAmount;
    }

    IEnumerator ErrorPopUpTextFade() {
        yield return StartCoroutine(Fade(1)); // Fade in
        yield return new WaitForSecondsRealtime(3f);
        yield return StartCoroutine(Fade(0)); // Fade out
    }

    IEnumerator Fade(float targetAlpha) {
        float startAlpha = error.color.a;
        float elapsedTime = 0f;
        Color currentColor = error.color;

        while (elapsedTime < 1f)
        {
            currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / 1f);
            error.color = currentColor;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        currentColor.a = targetAlpha;
        error.color = currentColor;
    }
    
}