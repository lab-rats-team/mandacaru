using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour {
	
	public Animator anim;
	public AnimationClip fadeOutClip;

	public void FadeToScene(int sceneIdx) {
		StartCoroutine(Fade(sceneIdx));
	}

	IEnumerator Fade(int sceneIdx) {
		anim.SetTrigger("fade_out");
		yield return new WaitForSecondsRealtime(fadeOutClip.length);
		Time.timeScale = 1f;
		SceneManager.LoadScene(sceneIdx);
	}

}
