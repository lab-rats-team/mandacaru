using System;

public class Configs {
	private bool english, fullscreen;
	private float musicVol, sfxVol;

	public Configs() {}

	public Configs(bool en, bool fs, float msc, float sfx) {
		english = en;
		fullscreen = fs;
		musicVol = msc;
		sfxVol = sfx;
	}

	public bool GetEnglish() {
		return english;
	}

	public void SetEnglish(bool en) {
		english = en;
	}

	public bool GetFullscreen() {
		return fullscreen;
	}

	public void SetFullscreen(bool fs) {
		fullscreen = fs;
	}

	public float GetMusicVol() {
		return musicVol;
	}

	public void SetMusicVol(float msc) {
		musicVol = msc;
	}

	public float GetSfxVol() {
		return sfxVol;
	}

	public void SetSfxVol(float sfx) {
		sfxVol = sfx;
	}
}
