using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehavior : MonoBehaviour {
	PlayerInputActions _inputActions;

	void ToggleCursor(bool isVisible) {
		Cursor.visible = isVisible;
		Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
	}

	void Start() {
		_inputActions = new PlayerInputActions(); // Initialize input actions
		_inputActions.Enable(); // Enable input actions
		
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		// Create `ToggleCursor` event listeners to toggle cursor by holding "Alt" key
		_inputActions.PlayerActionmap.ToggleCursor.started += _ => ToggleCursor(true);
		_inputActions.PlayerActionmap.ToggleCursor.canceled += _ => ToggleCursor(false);
	}

	// Update is called once per frame
	//void Update() {}
}