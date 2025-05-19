using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]

public class Dialogue : ScriptableObject {
	[SerializeField]
	public List<dialogueType> dialogues;

	[System.Serializable]
	public class dialogueType {
		[SerializeField]
		public string Speaker;
		[SerializeField]
		public string Line;
	}
}