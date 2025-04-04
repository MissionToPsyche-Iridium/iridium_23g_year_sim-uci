using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehavior : MonoBehaviour {
	PlayerInputActions _inputActions;

	// Store delegates for subscription/unsubscription
	private System.Action<InputAction.CallbackContext> _startedCallback;
	private System.Action<InputAction.CallbackContext> _canceledCallback;

	public void ToggleCursor(bool isVisible) {
		Cursor.visible = isVisible;
		Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
	}

	public void EnableCursorListeners() {
		_inputActions.PlayerActionmap.ToggleCursor.started += _startedCallback;
		_inputActions.PlayerActionmap.ToggleCursor.canceled += _canceledCallback;
	}

	public void DisableCursorListeners() {
		_inputActions.PlayerActionmap.ToggleCursor.started -= _startedCallback;
		_inputActions.PlayerActionmap.ToggleCursor.canceled -= _canceledCallback;
	}

	void Start() {
		_inputActions = new PlayerInputActions();
		_inputActions.Enable();

		// Define callbacks once
		_startedCallback = ctx => ToggleCursor(true);
		_canceledCallback = ctx => ToggleCursor(false);

		EnableCursorListeners();

		ToggleCursor(false);
	}
}