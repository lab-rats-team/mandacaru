using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuService : MonoBehaviour {

	public GameObject[] popUps;

	public void openPopUp(int popUpIdx) {
		closeAllPopUps();
		popUps[popUpIdx].SetActive(true);
	}

    public void quit() => Application.Quit();

	private void closeAllPopUps() {
		foreach (GameObject popUp in popUps) {
			popUp.SetActive(false);
		}
	}

}
