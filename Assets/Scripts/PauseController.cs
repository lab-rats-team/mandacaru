using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour {
	
	public GameObject[] popUps;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale == 1f) {
				Time.timeScale = 0f;
				OpenPopUp("PausePopUp");
			} else if (FindPopUp("PausePopUp").activeSelf) {
				Resume();
			} else {
				OpenPopUp("PausePopUp");
			}
		}
    }
	
	public void Resume() {
		CloseAllPopUps();
		Time.timeScale = 1f;
	}
	
	public void Exit() => Application.Quit();
	
	public void ReturnToMainMenu() => SceneManager.LoadScene(0);
	
	public void OpenPopUp(string popUpName) {
		CloseAllPopUps();
		FindPopUp(popUpName).SetActive(true);
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
