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

	public void Start() {
		CloseAllPopUps();
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

	public void setEnglish(bool english) {
		// chamar tradutor
		ptCircle.GetComponent<Image>().sprite =  english ? emptyCircle : filledCircle;
		enCircle.GetComponent<Image>().sprite = !english ? emptyCircle : filledCircle;
	}

	public void setFullscreen(bool fullscreen) {
		// alterar janela
		winCircle.GetComponent<Image>().sprite = fullscreen ? emptyCircle : filledCircle;
		fsCircle.GetComponent<Image>().sprite = !fullscreen ? emptyCircle : filledCircle;
	}

}
