using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {
	
	public GameObject[] popUps;

	public GameObject ptButton;
	public GameObject enButton;
	public GameObject ptCircle;
	public GameObject enCircle;

	public GameObject fsButton;
	public GameObject winButton;
	public GameObject fsCircle;
	public GameObject winCircle;

	public Sprite emptyCircle;
	public Sprite filledCircle;

	public Slider mscSlider;
	public Slider sfxSlider;

	public PaperPieceManager piecesManager;
	public Fader screenFader;
	
	private ConfigsModel currentConfigs;
	
	void Awake() {
		bool english = PlayerPrefs.GetInt("english", 0) == 0 ? false : true;
		bool fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 0 ? false : true;
		float music = PlayerPrefs.GetFloat("musicVol", 1f);
		float sfx = PlayerPrefs.GetFloat("sfxVol", 1f);
		currentConfigs = new ConfigsModel(english, fullscreen, music, sfx);
		
		UpdateConfigs(currentConfigs);
		Resume();
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale == 1f) {
				piecesManager.SlideIn();
				Time.timeScale = 0f;
				OpenPopUp("PausePopUp");
			} else if (FindPopUp("PausePopUp").activeSelf) {
				piecesManager.SlideOff();
				Resume();
			} else {
				OpenPopUp("PausePopUp");
			}
		}
    }
	
	public void Resume() {
		CloseAllPopUps();
		Time.timeScale = 1f;
	}

	public void SaveConfigs() {
		ConfigsModel cfg = GetConfigs();
		ApplyFullScreen(cfg.GetFullscreen());
		AudioManager.instance.UpdateSoundsVolume(cfg.GetMusicVol(), cfg.GetSfxVol());
		LanguageManager.instance.LoadLanguage(cfg.GetEnglish() ? "en" : "pt");

		PlayerPrefs.SetInt("fullscreen", cfg.GetFullscreen() ? 1 : 0);
		PlayerPrefs.SetFloat("musicVol", cfg.GetMusicVol());
		PlayerPrefs.SetFloat("sfxVol", cfg.GetSfxVol());
		PlayerPrefs.SetInt("english", cfg.GetEnglish() ? 1 : 0);
		PlayerPrefs.Save();

		currentConfigs = cfg;
	}

	public ConfigsModel GetConfigs() {
		ConfigsModel cfg = new ConfigsModel();
		cfg.SetEnglish(enCircle.GetComponent<Image>().sprite == filledCircle);
		cfg.SetFullscreen(fsCircle.GetComponent<Image>().sprite == filledCircle);
		cfg.SetMusicVol(mscSlider.value);
		cfg.SetSfxVol(sfxSlider.value);
		return cfg;
	}

	public void SetEnglishUI(bool english) {
		ptCircle.GetComponent<Image>().sprite =  english ? emptyCircle : filledCircle;
		enCircle.GetComponent<Image>().sprite = !english ? emptyCircle : filledCircle;
	}

	public void SetFullscreenUI(bool fullscreen) {
		winCircle.GetComponent<Image>().sprite = fullscreen ? emptyCircle : filledCircle;
		fsCircle.GetComponent<Image>().sprite = !fullscreen ? emptyCircle : filledCircle;
	}

	public void UpdateConfigs(ConfigsModel cfg) {
		SetEnglishUI(cfg.GetEnglish());
		SetFullscreenUI(cfg.GetFullscreen());
		mscSlider.value = cfg.GetMusicVol();
		sfxSlider.value = cfg.GetSfxVol();
	}
	
	public void Exit() => Application.Quit();
	
	public void ReturnToMainMenu() => screenFader.FadeToScene(0);
	
	private void ApplyFullScreen(bool fullScreen) {
		Resolution maxResol = Screen.resolutions[Screen.resolutions.Length - 1];
		if (fullScreen)
			Screen.SetResolution(maxResol.width, maxResol.height, true);
		else
			Screen.SetResolution((int)Mathf.Round(maxResol.width / 1.7f), (int)Mathf.Round(maxResol.height / 1.7f), false);
	}
	
	public void OpenPopUp(string popUpName) {
		CloseAllPopUps();
		FindPopUp(popUpName).SetActive(true);
	}

	public void CloseAllPopUps() {
		foreach (GameObject popUp in popUps) {
			popUp.SetActive(false);
		}
	}
	
	private GameObject FindPopUp(string name) {
		foreach (GameObject popUp in popUps)
			if (popUp.name == name) return popUp;

		return null;
	}
	
}
