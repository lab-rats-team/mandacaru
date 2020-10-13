using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class LanguageManager : MonoBehaviour {
	public static LanguageManager instance;

	public static Dictionary<String, String> fields;
	[HideInInspector] public UnityEvent translateEvent;

	private string loadedLang;

	void Awake() {
		if (instance == null) {
			instance = this;
			translateEvent = new UnityEvent();
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	public void LoadLanguage(string lang) {
		if (fields == null)
			fields = new Dictionary<string, string>();
		else if (loadedLang == lang)
			return;

		fields.Clear();


		if (PlayerPrefs.GetInt("_language_index", -1) == -1)
			PlayerPrefs.SetInt("_language_index", 0);

		string allTexts = (Resources.Load(@"Languages/" + lang) as TextAsset).text;

		string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
		string key, value;

		for (int i = 0; i < lines.Length; i++) {
			if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#")) {
				key = lines[i].Substring(0, lines[i].IndexOf("="));
				value = lines[i].Substring(lines[i].IndexOf("=") + 1,
					lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
				fields.Add(key, value);
			}
		}
		loadedLang = lang;

		translateEvent.Invoke();
	}

	public static string GetTraduction(string key) {
		if (!fields.ContainsKey(key)) {
			Debug.LogError("Não existe uma chave com nome: [" + key + "] nos arquivos de texto");
			return null;
		}

		return fields[key];
	}


}
