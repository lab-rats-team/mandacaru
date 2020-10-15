using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {
	
	public GameObject[] popUps;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale == 1f) {
				Time.timeScale = 0f;
				FindPopUp("PausePopUp").SetActive(true);
			} else if (FindPopUp("PausePopUp").activeSelf) {
				FindPopUp("PausePopUp").SetActive(false);
				Time.timeScale = 1f;
			} else {
				CloseAllPopUps();
				FindPopUp("PausePopUp").SetActive(true);
			}
		}
    }

	public void CloseAllPopUps() {
		foreach (GameObject popUp in popUps) {
			popUp.SetActive(false);
		}
	}
	
	private GameObject FindPopUp(string name) {
		foreach (GameObject popUp in popUps)
			if (popUp.name == name) return popUp;

		return null;
	}
	
}
