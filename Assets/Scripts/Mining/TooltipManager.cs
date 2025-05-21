using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] private Vector2 screenOffset = new Vector2(60, 60); // Adjust as needed
    public static TooltipManager _instance;
    public TextMeshProUGUI textComponent;

    private Transform followTarget = null; // The world object to follow

    public void Awake() {
        if (_instance != null && !_instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    void Start() {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    void Update() {
        if (followTarget != null) {
            // Add offset to the screen position
            transform.position = Camera.main.WorldToScreenPoint(followTarget.position) + (Vector3)screenOffset;
        }
    }

    public void SetAndShowToolTip(string text, Transform worldObject) {
        gameObject.SetActive(true);
        textComponent.text = text;
        followTarget = worldObject;
        // Set initial position with offset
        transform.position = Camera.main.WorldToScreenPoint(worldObject.position) + (Vector3)screenOffset;
    }

    public void HideToolTip() {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
        followTarget = null;
    }
}