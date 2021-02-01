using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoader : MonoBehaviour {

	public string[] ignoredScenes;

	[HideInInspector]
	static public SaveLoader instance;
	[HideInInspector]
	public int currentSaveIdx;
	private SaveModel currentSave;

	void Awake() {
		if (instance == null) {
			instance = this;
			SceneManager.sceneLoaded += OnSceneLoad;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode mode) {
		foreach(string s in ignoredScenes)
			if(s == scene.name) return;

		currentSave = LoadSave(currentSaveIdx);
	}

	public SaveModel CreateSave(int saveIdx) {
		SaveModel save = new SaveModel();
		string path = Path.Combine(Application.persistentDataPath, "save" + saveIdx + ".bin"); // Path.Combine coloca uma barra '/' ou contra-barra '\' entre as string dependendo da plataforma
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = new FileStream(path, FileMode.Create);
		formatter.Serialize(file, save);
		file.Close();
		return save;
	}

	public void EraseSave(int saveIdx) {
		string path = Path.Combine(Application.persistentDataPath, "save" + saveIdx + ".bin");
		File.Delete(path);
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

	public void UpdateSave(float x, float y, int hp) {
		currentSave.SetPlayerX(x);
		currentSave.SetPlayerY(y);
		currentSave.SetPlayerHp(hp);
		string path = Path.Combine(Application.persistentDataPath, "save" + currentSaveIdx + ".bin");
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = new FileStream(path, FileMode.OpenOrCreate);
		formatter.Serialize(file, currentSave);
		file.Close();
	}

	public Vector3 GetPlayerPosition() {
		float x = currentSave.GetPlayerX();
		float y = currentSave.GetPlayerY();
		float z = currentSave.GetPlayerZ();
		return new Vector3(x, y, z);
	}

	public int GetPlayerHp() => currentSave.GetPlayerHp();

}
