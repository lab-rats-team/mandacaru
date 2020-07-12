using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public Sound[] sounds;

	private float globalSfxVolume;
	private float globalMusicVolume;

	void Awake () {

		if (instance == null)
			instance = this;
		else {
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			//s.source.minDistance = s.minDistance;
			//s.source.maxDistance = s.maxDistance;
			s.source.loop = s.loop;
		}
	}

	public void Play(string soundName) {
		Sound s = Array.Find(sounds, sound => sound.name == soundName);
		if (s == null)
			Debug.LogWarning("Clipe de áudio \"" + soundName + "\" não encontrado.");
		else {
			s.source.volume = s.volume * (s.type == SoundType.Music ? globalMusicVolume : globalSfxVolume);
			Debug.Log("Name = " + s.name + " and volume = " + s.source.volume);
			s.source.Play();
		}
	}

	public void UpdateSoundsVolume(float mscVol, float sfxVol) {
		globalMusicVolume = mscVol;
		globalSfxVolume = sfxVol;
		foreach (Sound s in sounds) 
			s.source.volume = s.volume * (s.type == SoundType.Music ? globalMusicVolume : globalSfxVolume);
	}

}

[System.Serializable]
public enum SoundType {
	Music,
	Sfx
}
