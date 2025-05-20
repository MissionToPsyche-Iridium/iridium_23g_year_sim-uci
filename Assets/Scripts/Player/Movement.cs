using UnityEngine;

public abstract class Movement : MonoBehaviour {
	#region --
	PlayerInputActions _inputActions;

	[SerializeField]
	protected MoveData moveData;

	public Transform planet;
	[SerializeField]
	Transform transformBody;

	protected Vector3 movementVector;
	protected Vector3 gravityDirection;
	public float gravityStrength = 1f;
	protected Vector3 jumpVector;

	[SerializeField]
	LayerMask GroundLayerMask;
	[SerializeField]
	Transform groundCollider;
	[SerializeField]
	Transform cameraTransform; // Reference to camera
	[SerializeField]
	public GameObject model;

	RayData groundData;
	#endregion

	void Start() {
		groundData = new RayData();

		_inputActions = new PlayerInputActions();
		_inputActions.Enable();
	}

    void OnDisable()
    {
        _inputActions.Disable();
    }

    void Update() {
		ApplyGravity();
		CheckGround();
		LookAtCamera();
		RotateToSurface();
		Move();
	}
    void ApplyGravity() {
		gravityDirection = (planet.position - transform.position).normalized;
		if (!groundData.grounded) { //Limits max gravitational pull increase
			if (gravityStrength <= 10) {
				gravityStrength += planet.GetComponent<Planet>().GravitationalPull * Time.deltaTime;
			} else {
				gravityStrength = 10;
			}
		} else {
			gravityStrength = moveData.surfaceGravity;
		}
	}

	void RotateToSurface() {
		Quaternion gravityRotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
		Quaternion surfaceRotation = Quaternion.FromToRotation(transform.up, groundData.normal) * transform.rotation;
		Quaternion finalRotation = Quaternion.Lerp(gravityRotation, surfaceRotation, moveData.stickToSurface);

		transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, moveData.surfaceRotationSpeed * Time.deltaTime);
	}

	void Move() {
		Vector2 input = _inputActions.PlayerActionmap.Movement.ReadValue<Vector2>();

		if (groundData.grounded) {
			Vector3 camForward = cameraTransform.forward;
			Vector3 camRight = cameraTransform.right;

			// Project camera directions onto the movement plane
			camForward = Vector3.ProjectOnPlane(camForward, groundData.normal).normalized;
			camRight = Vector3.ProjectOnPlane(camRight, groundData.normal).normalized;

			// Apply movement relative to camera direction
			movementVector = (camForward * input.y + camRight * input.x) * moveData.moveSpeed;
		}
	}

	void CheckGround() {
		if (Physics.CheckSphere(groundCollider.position, moveData.groundColSize, GroundLayerMask)) {
			Physics.Raycast(groundCollider.position, -transform.up, out RaycastHit hit, 5f);
			groundData.grounded = true;
			groundData.normal = hit.normal;
			return;
		}

		groundData.grounded = false;
		groundData.normal = -gravityDirection;
	}
	
	void LookAtCamera() {
		model.transform.localRotation = new Quaternion(0, cameraTransform.localRotation.y, 0, cameraTransform.localRotation.w);
	}
}