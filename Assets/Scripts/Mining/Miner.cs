using UnityEngine;
using System.Collections.Generic;

public class Miner : MonoBehaviour
{
    // [SerializeField] Mineable currentMine = null; // Current mineable resource
    [SerializeField] private List<Mineable> nearbyMines = new List<Mineable>();
    [SerializeField] MiningUI mineUI = null; // UI for mining

    public delegate void OnEnterMine(Mineable m); // Delegate for when a mine is entered
    public OnEnterMine onEnterMine; // Event for when a mine is entered
    
    public delegate void OnExitMine(); // Delegate for when a mine is exited
    public OnExitMine onExitMine; // Event for when a mine is exited

    // Initialize the UI when the game starts
    private void Start() {
      mineUI.initializeUI();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Mine")) {
            Mineable m;
            if (other.TryGetComponent(out m)) {
                if (!nearbyMines.Contains(m)) {
                    nearbyMines.Add(m);
                    // Debug.Log("Added: " + m.name + " | Count: " + nearbyMines.Count);
                    m.EnterMine(this);
                    onEnterMine?.Invoke(m);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Mine")) {
            Mineable m;
            if (other.TryGetComponent(out m)) {
                if (nearbyMines.Contains(m)) {
                    m.ExitMine();
                    nearbyMines.Remove(m);
                    // Debug.Log("Removed: " + m.name + " | Count: " + nearbyMines.Count);
                    onExitMine?.Invoke();
                }

                // NEW: Update UI to the new closest mine if any remain
                Mineable closest = ClosestMineInRange();
                if (closest != null) {
                    onEnterMine?.Invoke(closest);
                }
            }
        }
    }

    // Mine only where cursor is
    // Add Layer "Ignore Raycast" to Player
    // SolarCamera -> "Untagged" Tag
    // Psyche > PsycheCenter > Player > CameraRig > Camera -> "MainCamera" Tag
    public Mineable MineUnderCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.SphereCast(ray, .5f, out hit, 100f)) {
            Mineable m;
            if (hit.collider.TryGetComponent(out m)) {
                if (nearbyMines.Contains(m)) {
                    return m;
                }
            }
        }
        return null;
    }

    public Mineable ClosestMineInRange() {
        if (nearbyMines.Count == 0) return null;
        Mineable closest = null;
        float minDist = float.MaxValue;
        Vector3 myPos = transform.position;
        foreach (var m in nearbyMines) {
            float dist = Vector3.Distance(myPos, m.transform.position);
            if (dist < minDist) {
                minDist = dist;
                closest = m;
            }
        }
        return closest;
    }

    // Called when rover mines the resource
    #region Public Methods
    public void MineCurrent() {
        Mineable m = ClosestMineInRange();
        if (m != null) {
            m.MineResource();
        }
    }
    #endregion
}