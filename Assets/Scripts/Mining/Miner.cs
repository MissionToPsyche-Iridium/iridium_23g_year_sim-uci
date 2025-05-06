using UnityEngine;
// using Inventory;

public class Miner : MonoBehaviour
{
    // [SerializeField] Inventory inventory = null;
    [SerializeField] int resource = 0;
    [SerializeField] Mineable currentMine = null;
    [SerializeField] MiningUI mineUI = null;

    public delegate void OnEnterMine(Mineable m);
    public OnEnterMine onEnterMine;
    
    public delegate void OnExitMine();
    public OnExitMine onExitMine;

    private void Start() {
      mineUI.initializeUI();
    }

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Mine")) {
        Mineable m;
        if (other.TryGetComponent(out m)) {
          EnterMine(m);
        }
      }
    }

    private void OnTriggerExit(Collider other) {
      if (other.CompareTag("Mine")) {
        ExitMine();
      }
    }

    private void EnterMine(Mineable m) {
      currentMine = m;
      currentMine.EnterMine(this);
      onEnterMine?.Invoke(currentMine);
    }

    private void ExitMine() {
      if (currentMine) {
        currentMine.ExitMine();
      }
      currentMine = null;
      onExitMine?.Invoke();
    }

    #region Public Methods
    public void MineCurrent() {
      if (currentMine) {
        currentMine.MineResource();
      }
    }

    // public void ReceiveResource(Item i) {
    //   if (inventory) {
    //     inventory.AddToInventory(i);
    //   }
    //   else {
    //     Debug.LogError("No inventory found on miner: " + gameObject.name);
    //   }
    // }
    #endregion
}