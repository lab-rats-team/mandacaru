using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MenuController : MonoBehaviour {

	private MenuView view;
	private ConfigsModel currentConfigs;
	private SaveModel[] saves;

	void Awake() {
		view = gameObject.GetComponent<MenuView>();

		bool english = PlayerPrefs.GetInt("english", 0) == 0 ? false : true;
		bool fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 0 ? false : true;
		float music = PlayerPrefs.GetFloat("musicVol", 1f);
		float sfx = PlayerPrefs.GetFloat("sfxVol", 1f);
		currentConfigs = new ConfigsModel(english, fullscreen, music, sfx);

		saves = new SaveModel[3];
		for(int i = 0; i < saves.Length; i++)
			saves[i] = LoadSave(i);
	}

	void Start() {
		view.CloseAllPopUps();
		view.UpdateConfigs(currentConfigs);
		view.UpdateSaves(saves);
		ApplyFullScreen(PlayerPrefs.GetInt("fullscreen") == 1);
		AudioManager.instance.UpdateSoundsVolume(currentConfigs.GetMusicVol(), currentConfigs.GetSfxVol());
		//AudioManager.instance.Play("introduction");
		LanguageManager.instance.LoadLanguage(currentConfigs.GetEnglish() ? "en" : "pt");
	}

	public void SaveConfigs() {
		ConfigsModel cfg = view.GetConfigs();
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
	
	public void ResetConfigs() => view.UpdateConfigs(currentConfigs);

	public void CreateSave(int saveIdx) {
		saves[saveIdx] = new SaveModel();
		string path = Path.Combine(Application.persistentDataPath, "save" + saveIdx + ".bin"); // Path.Combine coloca uma barra '/' ou contra-barra '\' entre as string dependendo da plataforma

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = new FileStream(path, FileMode.Create);
		formatter.Serialize(file, saves[saveIdx]);
		file.Close();
		SceneManager.LoadScene(saves[saveIdx].GetLevelId());
	}

	public void EraseSave(int saveIdx) {
		string path = Path.Combine(Application.persistentDataPath, "save" + saveIdx + ".bin");
		File.Delete(path);
		saves[saveIdx] = null;
		view.UpdateSaves(saves);
	}

	public SaveModel LoadSave(int saveIdx) {
		string path = Path.Combine(Application.persistentDataPath, "save" + saveIdx + ".bin");
		if (File.Exists(path)) {

			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = new FileStream(path, FileMode.Open);

			SaveModel save = formatter.Deserialize(file) as SaveModel;
			file.Close();
			return save;
		}
		return null;
	}

	public void ContinueGame(int saveIdx) {
		if (saves[saveIdx] == null) {
			Debug.LogError("Erro ao tentar entrar em jogo não salvo: slot " + (saveIdx + 1));
		} else {
			SceneManager.LoadScene(saves[saveIdx].GetLevelId());
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
