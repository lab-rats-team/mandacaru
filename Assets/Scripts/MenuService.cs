using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class MenuService : MonoBehaviour {

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

	private Configs currentConfigs;

	public void Start() {
		CloseAllPopUps();
		currentConfigs = new Configs(false, true, 0.5f, 0.5f); // temporário
		UpdateConfigUI();
	}

	public void OpenPopUp(int popUpIdx) {
		CloseAllPopUps();
		try {
			popUps[popUpIdx].SetActive(true);
		} catch (IndexOutOfRangeException e) {
			Debug.LogError("Falha ao tentar abrir pop-up com index inválido: " + e.Message);
		}
	}

    public void Quit() => Application.Quit();

	public void CloseAllPopUps() {
		foreach (GameObject popUp in popUps) {
			popUp.SetActive(false);
		}
	}

	public void SetEnglishUI(bool english) {
		// chamar tradutor
		ptCircle.GetComponent<Image>().sprite =  english ? emptyCircle : filledCircle;
		enCircle.GetComponent<Image>().sprite = !english ? emptyCircle : filledCircle;
	}

	public void SetFullscreenUI(bool fullscreen) {
		winCircle.GetComponent<Image>().sprite = fullscreen ? emptyCircle : filledCircle;
		fsCircle.GetComponent<Image>().sprite = !fullscreen ? emptyCircle : filledCircle;
	}

	public Configs GetConfigsFromUI() {
		Configs cfg = new Configs();
		cfg.SetEnglish(enCircle.GetComponent<Image>().sprite == filledCircle);
		cfg.SetFullscreen(fsCircle.GetComponent<Image>().sprite == filledCircle);
		cfg.SetMusicVol(mscSlider.value);
		cfg.SetSfxVol(sfxSlider.value);
		return cfg;
	}

	public void UpdateConfigUI() {
		SetEnglishUI(currentConfigs.GetEnglish());
		SetFullscreenUI(currentConfigs.GetFullscreen());
		mscSlider.value = currentConfigs.GetMusicVol();
		sfxSlider.value = currentConfigs.GetSfxVol();
	}

	public void SaveConfigs() {
		Configs cfg = GetConfigsFromUI();
		// setar janela e volumes
		currentConfigs = cfg;
	}

}
