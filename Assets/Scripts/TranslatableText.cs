using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TranslatableText : MonoBehaviour {

	public string key;

	void Start() {
		LanguageManager.instance.translateEvent.AddListener(new UnityAction(Translate));
	}

	void Translate() {
		GetComponent<TMPro.TMP_Text>().text = LanguageManager.GetTraduction(key);
	}

	void OnEnable() {
		try {
			Translate();
		} catch (System.NullReferenceException e) {
			return;
		}
	}

}
