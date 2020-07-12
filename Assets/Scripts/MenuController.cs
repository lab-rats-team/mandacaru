using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	private MenuView view;
	private ConfigsModel currentConfigs;

	void Awake() {
		view = gameObject.GetComponent<MenuView>();

		bool english = PlayerPrefs.GetInt("english", 0) == 0 ? false : true;
		bool fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 0 ? false : true;
		float music = PlayerPrefs.GetFloat("musicVol", 1f);
		float sfx = PlayerPrefs.GetFloat("sfxVol", 1f);
		currentConfigs = new ConfigsModel(english, fullscreen, music, sfx);
	}

	void Start() {
		view.CloseAllPopUps();
		view.UpdateConfigs(currentConfigs);
		ApplyFullScreen(PlayerPrefs.GetInt("fullscreen") == 1);
		AudioManager.instance.UpdateSoundsVolume(currentConfigs.GetMusicVol(), currentConfigs.GetSfxVol());
		AudioManager.instance.Play("introduction");
	}

	public void SaveConfigs() {
		ConfigsModel cfg = view.GetConfigs();
		ApplyFullScreen(cfg.GetFullscreen());
		PlayerPrefs.SetInt("fullscreen", cfg.GetFullscreen() ? 1 : 0);
		AudioManager.instance.UpdateSoundsVolume(cfg.GetMusicVol(), cfg.GetSfxVol());
		// setar idioma
		currentConfigs = cfg;
		PlayerPrefs.Save();
	}

	public void Quit() => Application.Quit();
	
	private void ApplyFullScreen(bool fullScreen) {
		Resolution maxResol = Screen.resolutions[Screen.resolutions.Length - 1];
		if (fullScreen)
			Screen.SetResolution(maxResol.width, maxResol.height, true);
		else
			Screen.SetResolution((int)Mathf.Round(maxResol.width / 1.7f), (int)Mathf.Round(maxResol.height / 1.7f), false);
	}
}
