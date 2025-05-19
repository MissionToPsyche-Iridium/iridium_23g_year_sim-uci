using UnityEngine;
using System.Collections.Generic; // Needed for Dictionary

public enum SoundType { // Must match element order in the Inspector
	DIALOGUE_CHARACTER_BEEP,
	GAME_AMBIENCE,
	MENU_THEME,
	MINED,
	MINING,
	SELECT
}

[RequireComponent(typeof(AudioSource))] // Ensure sound manager always has an audio source component on it

public class SoundManager : MonoBehaviour {
	[SerializeField] private AudioClip[] soundList; // Pass audio clip object in the Inspector using `[SerializeField]`
	private static SoundManager instance;
	private Dictionary<SoundType, AudioSource> audioSources = new Dictionary<SoundType, AudioSource>(); // One AudioSource per sound type

	private void Awake() {
		// Check for pre-existing instance of `SoundManager` object
		if (instance != null && instance != this) {
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject); // Prevent object from being destroyed on scene change to facilitate music transitions

		// Initialize and assign one AudioSource per sound type
		foreach (SoundType sound in System.Enum.GetValues(typeof(SoundType))) {
			AudioSource source = gameObject.AddComponent<AudioSource>();
			source.clip = soundList[(int)sound];
			source.playOnAwake = false;
			audioSources[sound] = source;
		}
	}

	public static void PlaySound(SoundType sound, float volume = 1) /* SoundType, Volume */ {
		var source = instance.audioSources[sound];
		source.loop = false;
		source.volume = volume;
		source.Play(); // Play clip using separate AudioSource
	}

	public static void StopSound(SoundType sound, float volume = 1) {
		var source = instance.audioSources[sound];
		source.Stop(); // Stop only this sound's AudioSource
	}

	public static void LoopSound(SoundType sound, float volume = 1) {
		var source = instance.audioSources[sound];
		source.loop = true;
		source.volume = volume;
		source.Play();
	}
}