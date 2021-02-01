using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoader : MonoBehaviour {

	[HideInInspector]
	static public SaveLoader instance;
	[HideInInspector]
	public int currentSave;

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
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
		Debug.Log("Loading save " + currentSave);
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

}
