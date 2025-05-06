using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

public class Mineable : MonoBehaviour
{
    Miner currentMiner = null;

    [SerializeField] private string currentResource = "Magnesium";
    [SerializeField] private UpgradesCarousel upgradesCarousel;
    [SerializeField] private int resource = 0;
    [SerializeField] private int minResources = 1;
    [SerializeField] private int maxResources = 5;
    [SerializeField] private float miningTime = 1.0f;
    private int resourcesRemaining = 0;
    private float countdown = 0f;

    public float MiningProgress => countdown / miningTime;

    public delegate void OnEmpty();
    public OnEmpty onEmpty;

    private void Start() {
      resourcesRemaining = minResources;
      upgradesCarousel = FindObjectOfType<UpgradesCarousel>();
      if (!upgradesCarousel) {
        Debug.LogError("UpgradesCarousel not found in the scene.");
      }
    }

    public void EnterMine(Miner m) {
      currentMiner = m;
    }

    public void ExitMine() {
      currentMiner = null;
    }

    public void MineResource() {
      if (resourcesRemaining == 0) {
        return;
      }

      countdown += Time.deltaTime * 1.0f;

      if (countdown >= miningTime) {
        countdown = 0f;
        resourcesRemaining--;
      }

      if (resourcesRemaining == 0) {  
        gameObject.SetActive(false);
        onEmpty?.Invoke();
        AddResource(1);
      }
    }

    public void AddResource(int amount) {
      if (upgradesCarousel == null) {
        Debug.LogError("UpgradesCarousel is not assigned.");
        return;
      }

      switch (currentResource) {
        case "Magnesium":
          upgradesCarousel.AddMaterial("Magnesium", amount);
          break;
        case "Iron":
          upgradesCarousel.AddMaterial("Iron", amount);
          break;
        case "Nickel":
          upgradesCarousel.AddMaterial("Nickel", amount);
          break;
        default:
          Debug.LogError("Unknown resource type: " + currentResource);
          break;
      }
    }
}
