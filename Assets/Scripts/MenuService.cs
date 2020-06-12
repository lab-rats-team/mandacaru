using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MenuService : MonoBehaviour {

	public GameObject[] popUps;

	public void Start() {
		CloseAllPopUps();
	}

	public void OpenPopUp(int popUpIdx) {
		CloseAllPopUps();
		try {
			popUps[popUpIdx].SetActive(true);
		} catch (IndexOutOfRangeException e) {
			Debug.Log("Falha ao tentar abrir pop-up com index inválido");
		}
	}

    public void Quit() => Application.Quit();

	private void CloseAllPopUps() {
		foreach (GameObject popUp in popUps) {
			popUp.SetActive(false);
		}
	}

}
