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

    // Mine only where cursor is
    // Add Layer "Ignore Raycast" to Player
    // SolarCamera -> "Untagged" Tag
    // Psyche > PsycheCenter > Player > CameraRig > Camera -> "MainCamera" Tag
    private bool CursorOverCurrentMine() {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;

      if (Physics.SphereCast(ray, .5f, out hit, 100f)) {
        print(hit.collider.name);

        Mineable m;
        if (hit.collider.TryGetComponent(out m)) {
          return m == currentMine;
        }
      }

      return false;
    }

    // Called when rover mines the resource
    #region Public Methods
    public void MineCurrent() {
      if (currentMine && CursorOverCurrentMine()) {
        currentMine.MineResource();
      }
    }
    #endregion
}