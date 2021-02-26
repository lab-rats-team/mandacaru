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
			//Debug.LogWarning("Há texto não traduzido em " + gameObject.name +
			//		" sob " + gameObject.transform.parent.gameObject.name);
			//Debug.LogWarning(e);
		}
	}

}
