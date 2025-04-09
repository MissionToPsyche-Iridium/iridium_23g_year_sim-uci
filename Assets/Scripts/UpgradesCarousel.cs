using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class UpgradesCarousel : MonoBehaviour {
    private int index = 0; // Tracks which page the user was on
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Image image;
    public TMP_Text type;
    public TMP_Text from;
    public TMP_Text to;

    public VerticalLayoutGroup descriptionGroup;
    private bool isMax = false; // If an upgrade has reached max upgrade level
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
    [SerializeField] public Sprite flashlightImage;

    [SerializeField] public string currentDrill = "Magnesium";
    [SerializeField] public int currentMiningSpeed = 10;
    [SerializeField] public int currentResourceMultiplier = 1;
    [SerializeField] public int currentLightStrength = 1;
    [SerializeField] public int magnesiumAmount = 0;
    [SerializeField] public int ironAmount = 0;
    [SerializeField] public int nickelAmount = 0;
    private Dictionary<string, int> matAmountsList;
    private Dictionary<int, (string type, Sprite typeImage, object current)> upgradeTypes;
    private Dictionary<string, (string next, Dictionary<string, int> requirements)> drillUpgrades;
    private Dictionary<int, (int next, Dictionary<string, int> requirements)> miningSpeedUpgrades;
    private Dictionary<int, (int next, Dictionary<string, int> requirements)> resourceMultiplierUpgrades;
    private Dictionary<int, (int next, Dictionary<string, int> requirements)> lightStrengthUpgrades;

    void Start() {
        matsList = new List<TMP_Text> { mat1, mat2, mat3 };
        reqsList = new List<TMP_Text> { req1, req2, req3 };

        upgradeTypes = new Dictionary<int, (string, Sprite, object)> {
            { 0, ("Drill", drillImage, currentDrill) },
            { 1, ("Mining Speed", miningSpeedImage, currentMiningSpeed) },
            { 2, ("Resource Multiplier", resourceMultiplierImage, currentResourceMultiplier) },
            { 3, ("Flashlight Strength", flashlightImage, currentLightStrength) }
        };

        drillUpgrades = new Dictionary<string, (string, Dictionary<string, int>)> {
            { "Magnesium", ("Reinforced Magnesium", new Dictionary<string, int> { 
                    { "Magnesium", 10 } 
                }) 
            },
            { "Reinforced Magnesium", ("Iron", new Dictionary<string, int> { 
                    { "Magnesium", 20 }, 
                    { "Iron", 10 }
                })
            },
            { "Iron", ("Nickel", new Dictionary<string, int> { 
                    { "Magnesium", 50 }, 
                    { "Iron", 30 }, 
                    { "Nickel", 10 } 
                })
            }
        };

        miningSpeedUpgrades = new Dictionary<int, (int next, Dictionary<string, int>)> {
            { 10, (8, new Dictionary<string, int> { 
                    { "Magnesium", 10 }
                }) 
            },
            { 8, (5, new Dictionary<string, int> { 
                    { "Magnesium", 25 },
                    { "Iron", 10 }
                }) 
            },
            { 5, (2, new Dictionary<string, int> { 
                    { "Iron", 30 },
                    { "Nickel", 15 }
                }) 
            }
        };

        resourceMultiplierUpgrades = new Dictionary<int, (int next, Dictionary<string, int>)> {
            { 1, (2, new Dictionary<string, int> { 
                    { "Magnesium", 20 }
                }) 
            },
            { 2, (5, new Dictionary<string, int> { 
                    { "Iron", 20 }
                }) 
            },
            { 5, (10, new Dictionary<string, int> { 
                    { "Nickel", 20 }
                }) 
            }
        };

        lightStrengthUpgrades = new Dictionary<int, (int next, Dictionary<string, int>)> {
            { 1, (2, new Dictionary<string, int> { 
                    { "Magnesium", 10 }
                }) 
            },
            { 2, (3, new Dictionary<string, int> { 
                    { "Iron", 10 }
                }) 
            }
        };

        matAmountsList = new Dictionary<string, int> { 
            { "Magnesium", magnesiumAmount},
            { "Iron", ironAmount},
            {"Nickel", nickelAmount }
        };
        
        maxUpgradeText = descriptionGroup.transform.GetChild(1);
        toFromGroup = descriptionGroup.transform.GetChild(2);
        requirementsGroup = descriptionGroup.transform.GetChild(3);

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

        resetMaterialsNeededInformation();

        if (checkIfMaxUpgradeReached(upgradeTypes[index].current)) {
            isMax = true;
            displayMaxUpgradeText();
            descriptionGroup.padding.bottom = 0;
        }
        else {
            isMax = false;
            displayMaxUpgradeText();
            descriptionGroup.padding.bottom = -85;
            descriptionSetUp();
        }
    }

    public void resetMaterialsNeededInformation() {
        mat1.gameObject.SetActive(false);
        mat2.gameObject.SetActive(false);
        mat3.gameObject.SetActive(false);
        req1.gameObject.SetActive(false);
        req2.gameObject.SetActive(false);
        req3.gameObject.SetActive(false);
    }

    public void displayMaxUpgradeText() { // Hides to and from description and requirements and unhides max upgrade reached text
        toFromGroup.gameObject.SetActive(!isMax);
        requirementsGroup.gameObject.SetActive(!isMax);
        maxUpgradeText.gameObject.gameObject.SetActive(isMax);
    }

    public void descriptionSetUp() { // Sets up & displays to, from, and material required descriptions // TODO: UPDATE WITH DICT
        switch (index) {
            case 1:
                from.text = upgradeTypes[index].current.ToString() + " seconds";
                to.text = miningSpeedUpgrades[currentMiningSpeed].next.ToString() + " seconds";
                materialRequiredSetUp(miningSpeedUpgrades[currentMiningSpeed].requirements);
                break;
            case 2:
                from.text = upgradeTypes[index].current.ToString() + " multiplier";
                to.text = resourceMultiplierUpgrades[currentResourceMultiplier].next.ToString() + " multiplier";
                materialRequiredSetUp(resourceMultiplierUpgrades[currentResourceMultiplier].requirements);;
                break;
            case 3:
                from.text = "Level " + upgradeTypes[index].current.ToString();
                to.text = "Level " + lightStrengthUpgrades[currentLightStrength].next.ToString();
                materialRequiredSetUp(lightStrengthUpgrades[currentLightStrength].requirements);
                break;
            default:
                from.text = upgradeTypes[index].current.ToString();
                to.text = drillUpgrades[currentDrill].next;
                materialRequiredSetUp(drillUpgrades[currentDrill].requirements);
                break;
        }
    }

    public void materialRequiredSetUp(Dictionary<string, int> requirements) { // Displays all materials required // TODO: UPDATE WITH DICT
        List<string> keys = requirements.Keys.ToList();

        for (int i = 0; i < keys.Count; i++) {
            matsList[i].gameObject.SetActive(true);
            matsList[i].text = keys[i];
            reqsList[i].gameObject.SetActive(true);
            reqsList[i].text =  matAmountsList[keys[i]] +  "/" + requirements[keys[i]].ToString();
        };
    }

    public bool checkIfMaxUpgradeReached(object currentUpgrade) {
        if (index == 0 && currentUpgrade is string mineral && mineral == "Nickel") { // Checks if drill upgrade max
            return true;
        }
        else if (index == 1 && currentUpgrade is int speed && speed == 2) { // Checks if mining speed upgrade max
            return true;
        }
        else if (index == 2 && currentUpgrade is int multiplier && multiplier == 10) { // Checks if resource multiplier upgrade max
            return true;
        }
        else if (index == 3 && currentUpgrade is int light && light == 2) { // Checks if flashlight strength upgrade max
            return true;
        }
        else {
            return false;
        }
    }

    public bool checkIfCanUpgrade(List<string> mats, List<int> amountRequired) { // Checks if user can upgrade
        for (int i = 0; i < mats.Count; i++) {
            // if (matAmountsList[i].gameObject != amountRequired[i]) { // TODO: Can't do this
            //     return false;
            // }
        }
        return true;
    }

    public void upgradeSelectedUpgrade() { // Upgrades the selected upgrade // TODO: UPDATE WITH DICT
        // switch (index) {
        //     case 0:
        //         if (checkIfCanUpgrade(drillUpgrades[currentDrill].mats, drillUpgrades[currentDrill].amountRequired)) {
        //             currentDrill = drillUpgrades[currentDrill].next;
        //             // TODO: Update mats
        //         }
        //         break;
        //     default:
        //         Debug.Log("Not Enough Mats To Upgrade");
        //         break;
        // }
    }

    public void resetMineralAmount() {
        // for (int i = 0; i < )
    }
}