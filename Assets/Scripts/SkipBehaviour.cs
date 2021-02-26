using System.Collections;
using UnityEngine;

public class SkipBehaviour : MonoBehaviour {

	public RuntimeAnimatorController animatorController;
	public KeyCode skipKey;
	public float appearTime;
	public int sceneToLoad;
	public Fader screenFader;

	private bool appeared;
	private SpriteRenderer sr;

    void Start() {
		sr = GetComponent<SpriteRenderer>();
		appeared = false;
    }

    void Update() {
        if (!appeared && Input.anyKey) {
			StartCoroutine("Appear");
			appeared = true;
		}

		if (appeared && Input.GetKeyDown(skipKey))
			screenFader.FadeToScene(sceneToLoad);
    }

	private IEnumerator Appear() {
		float s = Time.time;
		while (sr.color.a < 1f) {
			float a = (Time.time - s) / appearTime;
			sr.color = new Color(1f, 1f, 1f, a);
			yield return new WaitForEndOfFrame();
		}
	}
}
