using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

public class Mineable : MonoBehaviour
{
    Miner currentMiner = null;

    // [SerializeField] private Inventory.item resource = null;
    [SerializeField] private int resource = 0;

    [SerializeField] private int minResources = 1;
    [SerializeField] private int maxResources = 5;
    private int resourcesRemaining = 0;

    [SerializeField] private float miningTime = 1.0f;
    private float countdown = 0f;

    public float MiningProgress => countdown / miningTime;

    public delegate void OnEmpty();
    public OnEmpty onEmpty;

    private void Start() {
      resourcesRemaining = minResources;
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
      }

    }
}
