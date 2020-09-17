using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

	public string name;
	public AudioClip clip;
	public SoundType type;
	[HideInInspector]
	public AudioSource source;

	[Range(0f, 1f)]
	public float volume;
	//public float minDistance;
	//public float maxDistance;
	public bool loop;

}
