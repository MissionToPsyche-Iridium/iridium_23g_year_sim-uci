using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour {
	[SerializeField] public TextMeshProUGUI textbox;
	[SerializeField] public TextMeshProUGUI speakerbox;
	public Queue<string> lines = new Queue<string>();
	public string line;
	public Queue<string> speakers = new Queue<string>();
	public float textSpeed;

	public void SetLines(Dialogue dialogue) {
		foreach (var item in dialogue.dialogues) {
			speakers.Enqueue(item.Speaker);
			lines.Enqueue(item.Line);
		}
		gameObject.SetActive(true);
	}

	public void OnEnable() {
		textbox.text = string.Empty;
		StartDialogue();
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (textbox.text == line) {
				NextLine();
			} else {
				StopAllCoroutines();
				textbox.text = line;
			}
		}
	}

	void StartDialogue() {
		speakerbox.text = speakers.Dequeue();
		line = lines.Dequeue();
		StartCoroutine(TypeLine());
	}

	IEnumerator TypeLine() {
		foreach (char c in line.ToCharArray()) {
			textbox.text += c;
			SoundManager.PlaySound(SoundType.MINED);
			yield return new WaitForSecondsRealtime(textSpeed);
		}
	}

	void NextLine() {
		if (lines.Count > 0) {
			speakerbox.text = speakers.Dequeue();
			textbox.text = string.Empty;
			line = lines.Dequeue();
			StartCoroutine(TypeLine());
		} else { gameObject.SetActive(false); }
	}
}