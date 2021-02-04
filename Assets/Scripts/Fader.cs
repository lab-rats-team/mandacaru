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
		yield return new WaitForSeconds(fadeOutClip.length);
		SceneManager.LoadScene(sceneIdx);
	}

}
