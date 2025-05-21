using UnityEngine;
using UnityEngine.Events;

public class TriggerText : MonoBehaviour {
	[SerializeField] UnityEvent onTriggerEnter;
	public ResearchPaperLock paperLock;
	public PopUpManager popUpManager;
	private bool triggered = false;
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player"))
		{
			Debug.Log("Entered");
			onTriggerEnter.Invoke();
			var paperLock = FindObjectOfType<ResearchPaperLock>();
			var popUpManager = FindObjectOfType<PopUpManager>();

			if (paperLock != null && !paperLock.IsUnlocked("Psyche Shape"))
			{
				paperLock.UnlockPaper("Psyche Shape");
				paperLock.UnlockPaper("Temperature and Weather");
				if (popUpManager != null)
					popUpManager.CreatePopUp("Research Paper #4 & #5 is Unlocked");
				triggered = true;
			}
		}
	}
}