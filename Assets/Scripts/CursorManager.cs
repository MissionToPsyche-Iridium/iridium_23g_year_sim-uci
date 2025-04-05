using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

// TODO: Uncomment all lines related to cursor texture when sprite is assigned!

public class CursorManager : MonoBehaviour {
	//[SerializeField] private Texture2D cursorTexture;

	//private Vector2 cursorHotspot;
	public PlayerInputActions _inputActions;

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

	// Start is called before the first frame update
	void Start() {
		// Set custom cursor
		//cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
		//Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

		// Initialize input actions
		_inputActions = new PlayerInputActions();
		_inputActions.Enable();

		// Define listener callbacks once
		_startedCallback = ctx => ToggleCursor(true);
		_canceledCallback = ctx => ToggleCursor(false);

		// Initial state is hidden
		EnableCursorListeners();
		ToggleCursor(false);
	}
}