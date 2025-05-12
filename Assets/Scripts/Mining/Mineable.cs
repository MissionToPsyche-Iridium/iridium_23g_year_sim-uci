using UnityEngine;

public class Mineable : MonoBehaviour
{
    Miner currentMiner = null;

    [SerializeField] private ResourceType resourceType = ResourceType.Magnesium;
    [SerializeField] private UpgradesCarousel upgradesCarousel;
    [SerializeField] private int resource = 0;
    [SerializeField] private int minResources = 1;
    [SerializeField] private int resourceAmount = 1;
    private int resourcesRemaining = 0;
    private float countdown = 0f;
    public float MiningProgress => countdown / upgradesCarousel.currentMiningSpeed;

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

      countdown += Time.deltaTime * 1.0f; // Increase countdown based on time passed

      if (countdown >= upgradesCarousel.currentMiningSpeed) { // Mining is complete
        countdown = 0f;
        resourcesRemaining--;
      }

      if (resourcesRemaining == 0) {  // All resources have been mined -> delete the mineral
        gameObject.SetActive(false);
        onEmpty?.Invoke();
        AddResource(upgradesCarousel.currentResourceMultiplier);
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
}
