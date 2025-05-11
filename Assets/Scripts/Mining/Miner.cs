using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField] Mineable currentMine = null; // Current mineable resource
    [SerializeField] MiningUI mineUI = null; // UI for mining

    public delegate void OnEnterMine(Mineable m); // Delegate for when a mine is entered
    public OnEnterMine onEnterMine; // Event for when a mine is entered
    
    public delegate void OnExitMine(); // Delegate for when a mine is exited
    public OnExitMine onExitMine; // Event for when a mine is exited

    // Initialize the UI when the game starts
    private void Start() {
      mineUI.initializeUI();
    }

    // Called when rover enters a mineable resource
    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Mine")) {
        Mineable m;
        if (other.TryGetComponent(out m)) {
          EnterMine(m); // Enter the mineable resource
        }
      }
    }

    // Called when rover exits a mineable resource
    private void OnTriggerExit(Collider other) {
      if (other.CompareTag("Mine")) {
        ExitMine();
      }
    }

    // Called when rover enters a mineable resource
    private void EnterMine(Mineable m) {
      currentMine = m;
      currentMine.EnterMine(this);
      onEnterMine?.Invoke(currentMine);
    }

    // Called when rover exits a mineable resource
    private void ExitMine() {
      if (currentMine) {
        currentMine.ExitMine();
      }
      currentMine = null;
      onExitMine?.Invoke();
    }

    // Called when rover mines the resource
    #region Public Methods
    public void MineCurrent() {
      if (currentMine) {
        currentMine.MineResource();
      }
    }
    #endregion
}