using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	private MenuView view;
	private ConfigsModel currentConfigs;

	void Start() {
		view = gameObject.GetComponent<MenuView>();
		view.CloseAllPopUps();
		currentConfigs = new ConfigsModel(false, true, 0.5f, 0.5f); // temporário
		view.UpdateConfigs(currentConfigs);
		ApplyFullScreen(PlayerPrefs.GetInt("fullscreen") == 1);
	}

	public void SaveConfigs() {
		ConfigsModel cfg = view.GetConfigs();
		ApplyFullScreen(cfg.GetFullscreen());
		PlayerPrefs.SetInt("fullscreen", cfg.GetFullscreen() ? 1 : 0);
		// setar volumes e idioma
		currentConfigs = cfg;
		PlayerPrefs.Save();
	}

	public void Quit() => Application.Quit();
	
	private void ApplyFullScreen(bool fullScreen) {
		Resolution maxResol = Screen.resolutions[Screen.resolutions.Length - 1];
		if (fullScreen)
			Screen.SetResolution(maxResol.width, maxResol.height, true);
		else
			Screen.SetResolution((int)Mathf.Round(maxResol.width / 2), (int)Mathf.Round(maxResol.height / 2), false);
	}
}
