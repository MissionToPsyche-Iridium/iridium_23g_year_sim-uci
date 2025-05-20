using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class Mineable : MonoBehaviour
{
    Miner currentMiner = null;

    [SerializeField] private ResourceType resourceType = ResourceType.Magnesium;
    [SerializeField] private UpgradesCarousel upgradesCarousel;
    [SerializeField] private DrillType requiredDrill = DrillType.Magnesium;
    [SerializeField] private int resource = 0;
    [SerializeField] private int minResources = 1;
    [SerializeField] private int resourceAmount = 1;
    private bool beingMined = false;
    private int resourcesRemaining = 0;
    private float countdown = 0f;
    public float MiningProgress => countdown / upgradesCarousel.currentMiningSpeed;
    public TMP_Text error;

    public delegate void OnEmpty();
    public OnEmpty onEmpty;

    // Initialize number of resources
    private void Start() {
      resourcesRemaining = minResources;
      upgradesCarousel = FindObjectOfType<UpgradesCarousel>();
      if (!upgradesCarousel) {
        Debug.LogError("UpgradesCarousel not found in the scene.");
      }
    }

    // Reduce mining progress bar when not mining
    private void Update() {
      if (!beingMined && countdown > 0) {
        countdown -= Time.deltaTime;
      }

      beingMined = false;
    }

    // Rover enters the vicinity of a mineable resource
    public void EnterMine(Miner m) {
      currentMiner = m;
    }

    // Rover exits the vicinity of a mineable resource
    public void ExitMine() {
      currentMiner = null;
    }

    // Rover mines the resource
    public void MineResource() {
      if (resourcesRemaining == 0) { // No resources left to mine
        return;
      }

      if (!CanMineWithCurrentDrill) {
        StartCoroutine(ErrorPopUpTextFade());
        return;
      }

      beingMined = true;

      countdown += Time.deltaTime * 1.0f; // Increase countdown based on time passed

      if (countdown >= upgradesCarousel.currentMiningSpeed) { // Mining is complete
		    SoundManager.PlaySound(SoundType.MINED);
        countdown = 0f;
        resourcesRemaining--;
      }

      if (resourcesRemaining == 0) {  // All resources have been mined -> delete the mineral
        gameObject.SetActive(false);
        onEmpty?.Invoke();
        AddResource(upgradesCarousel.currentResourceMultiplier);
      }
    }

    // Check if the current drill can mine the resource
    private bool CanMineWithCurrentDrill {
      get {
        string currentDrill = upgradesCarousel.currentDrill;
        if (requiredDrill == DrillType.Magnesium) return true;
        else if (requiredDrill == DrillType.ReinforcedMagnesium) return currentDrill == "Reinforced Magnesium" || currentDrill == "Iron" || currentDrill == "Nickel";
        else if (requiredDrill == DrillType.Iron) return currentDrill == "Iron" || currentDrill == "Nickel";
        else {
          Debug.LogError("Unknown drill type");
          return false;
        }
      }
    }

    // Add resources to the UpgradesCarousel by amount
    public void AddResource(int amount) {
      if (upgradesCarousel == null) {
        Debug.LogError("UpgradesCarousel is not assigned.");
        return;
      }

      switch (resourceType) {
        case ResourceType.Magnesium:
          upgradesCarousel.AddMaterial("Magnesium", amount);
          break;
        case ResourceType.Iron:
          upgradesCarousel.AddMaterial("Iron", amount);
          break;
        case ResourceType.Nickel:
          upgradesCarousel.AddMaterial("Nickel", amount);
          break;
        default:
          Debug.LogError("Unknown resource type");
          break;
      }
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