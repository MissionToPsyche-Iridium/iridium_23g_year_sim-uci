using UnityEngine;
using UnityEngine.UI;

public class MiningUI : MonoBehaviour
{
    [SerializeField] private Miner miner = null; // Reference to the miner script
    private Mineable mine = null; // Reference to the mineable resource

    [SerializeField] private Image resourceImage = null; // Image for the resource being mined
    [SerializeField] private Image backgroundImage = null; // Background image for the resource being mined
    [SerializeField] private Image fillbarImage = null; // Fill bar image for the mining progress

    // Initialize the UI elements and set up event listeners
    public void initializeUI() {
      resourceImage.gameObject.SetActive(false); // Hide the resource image initially
      backgroundImage.gameObject.SetActive(false); // Hide the background image initially
      miner.onEnterMine += SetMine; // Set the mine when the miner enters a mineable resource
      miner.onExitMine += ExitMine; // Exit the mine when the miner exits a mineable resource
    }

    // Update the UI based on the mining progress
    private void Update() {
      if (!mine) {
        return;
      }

      // Update the mining progress
      UpdateUI(mine.MiningProgress);
    }

    // Set the mineable resource and update the UI
    private void SetMine(Mineable m) {
       mine = m;
    }

    // Update the UI elements based on the mining progress
    private void UpdateUI(float mineProgress) {
      resourceImage.gameObject.SetActive(mineProgress > 0); // Show the resource image if mining progress is greater than 0
      backgroundImage.gameObject.SetActive(mineProgress > 0); // Show the background image if mining progress is greater than 0

      fillbarImage.fillAmount = mineProgress; // Update the fill bar image based on the mining progress
    }

    // Exit the mineable resource and hide the UI elements
    private void ExitMine() {
      mine = null; 
      resourceImage.gameObject.SetActive(false);
      backgroundImage.gameObject.SetActive(false);
    }
}
