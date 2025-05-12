using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour {
	//[SerializeField] private Camera mainCamera; // TODO: Re-implement with Ivan/Queeny.
	[SerializeField] private Texture2D cursorTexture;

	private Vector2 cursorHotspot;
	// Store delegates for subscription/unsubscription
	private System.Action<InputAction.CallbackContext> _startedCallback;
	private System.Action<InputAction.CallbackContext> _canceledCallback;

	public PlayerInputActions _inputActions;

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

	public void ToggleMenuCursor(bool inMenu) {
		if (inMenu) {
			ToggleCursor(true);
			DisableCursorListeners();
		} else {
			ToggleCursor(false);
			EnableCursorListeners();
		}
	}

	// Start is called before the first frame update
	void Start() {
		// Set custom cursor
		cursorHotspot = new Vector2(7, 0); // Coordinates of selection point (pixels from top, left)
		//cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2); // Use to place hotspot in the center of sprite
		Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

		// Initialize input actions
		_inputActions = new PlayerInputActions();
		_inputActions.Enable();

		// Define listener callbacks once
		_startedCallback = ctx => { 
			ToggleCursor(true);
			//mainCamera.enabled = false;
		};

		_canceledCallback = ctx => {
			ToggleCursor(false);
			//mainCamera.enabled = true;
		};
	}
}