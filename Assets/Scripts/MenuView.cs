using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuView : MonoBehaviour {

	public GameObject[] popUps;

	public GameObject ptButton;
	public GameObject enButton;
	public GameObject ptCircle;
	public GameObject enCircle;

	public GameObject fsButton;
	public GameObject winButton;
	public GameObject fsCircle;
	public GameObject winCircle;

	public TMP_InputField emailInput;
	public TMP_InputField passInput;
	public TMP_Text authMsgLabel;

	public Sprite emptyCircle;
	public Sprite filledCircle;

	public Slider mscSlider;
	public Slider sfxSlider;

	public void SetEnglishUI(bool english) {
		ptCircle.GetComponent<Image>().sprite =  english ? emptyCircle : filledCircle;
		enCircle.GetComponent<Image>().sprite = !english ? emptyCircle : filledCircle;
	}

	public void SetFullscreenUI(bool fullscreen) {
		winCircle.GetComponent<Image>().sprite = fullscreen ? emptyCircle : filledCircle;
		fsCircle.GetComponent<Image>().sprite = !fullscreen ? emptyCircle : filledCircle;
	}

	public CredentialsModel GetCredentials() {
		string e = emailInput.text;
		string p = passInput.text;
		return new CredentialsModel(e, p);
	}

	public void SetAuthMessage(string msg) {
		authMsgLabel.text = msg;
	}

	public ConfigsModel GetConfigs() {
		ConfigsModel cfg = new ConfigsModel();
		cfg.SetEnglish(enCircle.GetComponent<Image>().sprite == filledCircle);
		cfg.SetFullscreen(fsCircle.GetComponent<Image>().sprite == filledCircle);
		cfg.SetMusicVol(mscSlider.value);
		cfg.SetSfxVol(sfxSlider.value);
		return cfg;
	}

	public void UpdateConfigs(ConfigsModel cfg) {
		SetEnglishUI(cfg.GetEnglish());
		SetFullscreenUI(cfg.GetFullscreen());
		mscSlider.value = cfg.GetMusicVol();
		sfxSlider.value = cfg.GetSfxVol();
	}

	public void UpdateSaves(SaveModel[] saves) {
		GameObject newGamePopUp = FindPopUp("NewGamePopUp");
		GameObject continuePopUp = FindPopUp("ContinuePopUp");
		GameObject deletePopUp = FindPopUp("DeletePopUp");
		Color stdColor = new Color(0f, 0f, 0f);
		Color disabledColor = new Color(.369f, .192f, .031f);
		for (int i = 0; i < saves.Length; i++) {

			Button ngButton =  newGamePopUp.transform.Find("Slot" + (i+1)).gameObject.GetComponent<Button>();
			Button ctButton = continuePopUp.transform.Find("Slot" + (i+1)).gameObject.GetComponent<Button>();
			Button delButton =  deletePopUp.transform.Find("Slot" + (i+1)).gameObject.GetComponent<Button>();

			if (saves[i] == null) {
				ngButton.interactable = true;
				ngButton.GetComponentInChildren<TMPro.TMP_Text>().color = stdColor;
				ctButton.interactable = false;
				ctButton.GetComponentInChildren<TMPro.TMP_Text>().color = disabledColor;
				delButton.interactable = false;
				delButton.GetComponentInChildren<TMPro.TMP_Text>().color = disabledColor;
			} else {
				ngButton.interactable = false;
				ngButton.GetComponentInChildren<TMPro.TMP_Text>().color = disabledColor;
				ctButton.interactable = true;
				ctButton.GetComponentInChildren<TMPro.TMP_Text>().color = stdColor;
				delButton.interactable = true;
				delButton.GetComponentInChildren<TMPro.TMP_Text>().color = stdColor;
			}
		}
	}

	private GameObject FindPopUp(string name) {
		foreach (GameObject popUp in popUps)
			if (popUp.name == name) return popUp;

		return null;
	}

	public void OpenPopUp(int idx) {
		CloseAllPopUps();
		try {
			popUps[idx].SetActive(true);
		} catch (IndexOutOfRangeException e) {
			Debug.LogError("Falha ao tentar abrir objeto com index inválido: " + e.Message);
		}
	}

	public void CloseAllPopUps() {
		foreach (GameObject popUp in popUps) {
			popUp.SetActive(false);
		}
	}

}
