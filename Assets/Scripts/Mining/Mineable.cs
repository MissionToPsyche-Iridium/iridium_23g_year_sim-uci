using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

public class Mineable : MonoBehaviour
{
    Miner currentMiner = null;

    [SerializeField] private int resource = 0; 
    [SerializeField] private int minResources = 1; // Minimum number of resources to mine (or how many progress bars that is filled)
    [SerializeField] private float miningTime = 1.0f;

    private int resourcesRemaining = 0;
    private float countdown = 0f;
    public float MiningProgress => countdown / miningTime;

    public delegate void OnEmpty();
    public OnEmpty onEmpty;

    // Initialize number of resources
    private void Start() {
      resourcesRemaining = minResources;
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
      SoundManager.PlaySound(SoundType.MINING);
      if (resourcesRemaining == 0) { // No resources left to mine
        return;
      }

      countdown += Time.deltaTime * 1.0f; // Increase countdown based on time passed

      if (countdown >= miningTime) { // Mining is complete
		SoundManager.PlaySound(SoundType.MINED);
        countdown = 0f;
        resourcesRemaining--;
      }

      if (resourcesRemaining == 0) {  // All resources have been mined -> delete the mineral
        gameObject.SetActive(false);
        onEmpty?.Invoke();
      }
    }
}