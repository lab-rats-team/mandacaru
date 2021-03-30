using UnityEngine;

public class MenuController : MonoBehaviour {

	public Fader screenFader;

	private MenuView view;
	private ConfigsModel currentConfigs;
	private CredentialsModel currentCredentials;
	private ServerAPI api;
	private SaveModel[] saves;

	void Awake() {
		view = gameObject.GetComponent<MenuView>();
		api = new ServerAPI();

		bool english = PlayerPrefs.GetInt("english", 0) == 0 ? false : true;
		bool fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 0 ? false : true;
		float music = PlayerPrefs.GetFloat("musicVol", 1f);
		float sfx = PlayerPrefs.GetFloat("sfxVol", 1f);
		currentConfigs = new ConfigsModel(english, fullscreen, music, sfx);
		currentCredentials = null;
	}

	void Start() {
		saves = new SaveModel[3];
		for(int i = 0; i < saves.Length; i++)
			saves[i] = SaveLoader.instance.LoadSave(i);

		view.CloseAllPopUps();
		view.OpenPopUp(5);
		view.UpdateConfigs(currentConfigs);
		view.UpdateSaves(saves);
		ApplyFullScreen(PlayerPrefs.GetInt("fullscreen") == 1);
		AudioManager.instance.UpdateSoundsVolume(currentConfigs.GetMusicVol(), currentConfigs.GetSfxVol());
		LanguageManager.instance.LoadLanguage(currentConfigs.GetEnglish() ? "en" : "pt");
		api.SetHeaderLanguage(currentConfigs.GetEnglish());
	}

	public async void LogIn() {
		CredentialsModel crd = view.GetCredentials();
		currentCredentials = crd;
		string msg = await api.LogIn(crd);
		if (msg == "OK")
			view.CloseAllPopUps();
		else
			view.SetAuthMessage(msg);
	}

	public async void SignIn() {
		CredentialsModel crd = view.GetCredentials();
		currentCredentials = crd;
		string msg = await api.SignIn(crd);
		if (msg == "OK")
			view.CloseAllPopUps();
		else
			view.SetAuthMessage(msg);
	}

	public void SaveConfigs() {
		ConfigsModel cfg = view.GetConfigs();
		ApplyFullScreen(cfg.GetFullscreen());
		AudioManager.instance.UpdateSoundsVolume(cfg.GetMusicVol(), cfg.GetSfxVol());
		LanguageManager.instance.LoadLanguage(cfg.GetEnglish() ? "en" : "pt");
		api.SetHeaderLanguage(cfg.GetEnglish());

		PlayerPrefs.SetInt("fullscreen", cfg.GetFullscreen() ? 1 : 0);
		PlayerPrefs.SetFloat("musicVol", cfg.GetMusicVol());
		PlayerPrefs.SetFloat("sfxVol", cfg.GetSfxVol());
		PlayerPrefs.SetInt("english", cfg.GetEnglish() ? 1 : 0);
		PlayerPrefs.Save();

		currentConfigs = cfg;
	}

	public void ResetConfigs() => view.UpdateConfigs(currentConfigs);

	public void DeleteSave(int saveIdx) {
		SaveLoader.instance.EraseSave(saveIdx);
		saves[saveIdx] = null;
		view.UpdateSaves(saves);
	}

	public void NewGame(int saveIdx) {
		saves[saveIdx] = SaveLoader.instance.CreateSave(saveIdx);
		ContinueGame(saveIdx);
	}

	public void ContinueGame(int saveIdx) {
		if (saves[saveIdx] == null) {
			Debug.LogError("Erro ao tentar entrar em jogo não salvo: slot " + (saveIdx + 1));
		} else {
			SaveLoader.instance.currentSaveIdx = saveIdx;
			screenFader.FadeToScene(saves[saveIdx].levelId);
		}
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
