using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {
	[Header("Gives input to these classes")]
	//[SerializeField] private SimpleInventory.UI.InventoryUI inventoryUI = null;
	//[SerializeField] private SimpleInventory.UI.CraftingUI craftingUI = null;
	//[SerializeField] private PointAndClickMover playerController = null;
	[SerializeField] private Miner miner = null;

	[Header("Inputs interpretted")]
	//[SerializeField] KeyCode inventoryKey = KeyCode.Tab;
	//[SerializeField] KeyCode moveKey = KeyCode.Mouse1;
	[SerializeField] KeyCode miningKey = KeyCode.Mouse0;

	private void Start() {}

	void Update() {
		// Mining handler
		if (Input.GetKey(miningKey)) {
			miner.MineCurrent();
		}
	}
}