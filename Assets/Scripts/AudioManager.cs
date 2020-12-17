using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		SceneManager.sceneLoaded += OnSceneLoaded;

		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			//s.source.minDistance = s.minDistance;
			//s.source.maxDistance = s.maxDistance;
			s.source.loop = s.loop;
		}

	}

	public void OnSceneLoaded(Scene s, LoadSceneMode loadMode) {
		foreach (Sound sound in sounds)
			sound.source.Stop();
		Sound sceneMusic = GetNthMusic(s.buildIndex);
		if (sceneMusic == null) {
			Debug.LogWarning("Quantidade de cenas maior que quantidade de músicas. A(s) última(s) cena(s) pode(m) ficar sem música.");
			return;
		}
		Play(sceneMusic.name);
	}

	public void Play(string soundName) {
		Sound s = Array.Find(sounds, sound => sound.name == soundName);
		if (s == null)
			Debug.LogWarning("Clipe de áudio \"" + soundName + "\" não encontrado.");
		else {
			s.source.volume = s.volume * (s.type == SoundType.Music ? globalMusicVolume : globalSfxVolume);
			s.source.Play();
		}
	}

	public void UpdateSoundsVolume(float mscVol, float sfxVol) {
		globalMusicVolume = mscVol;
		globalSfxVolume = sfxVol;
		foreach (Sound s in sounds) 
			s.source.volume = s.volume * (s.type == SoundType.Music ? globalMusicVolume : globalSfxVolume);
	}

	private Sound GetNthMusic(int n) {
		int i = 0;
		foreach (Sound s in sounds) {
			if (i == n) return s;
			if (s.type == SoundType.Music) i++;
		}
		return null;
	}

}

[System.Serializable]
public enum SoundType {
	Music,
	Sfx
}
