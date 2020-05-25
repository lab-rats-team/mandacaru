using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksCollisionHandler : MonoBehaviour
{

	public int breakPoint;
	public float fadeTime;

	private int currentSprite;
	private SpriteRenderer sr;
	private Animator anim;
	private BoxCollider2D boxColl;
	private Rigidbody2D rb;

	public void Awake() {
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		boxColl = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		currentSprite = 0;
	}

	public void OnTakeDamage() {
		currentSprite++;
		anim.SetTrigger("break");
		if (currentSprite == breakPoint) {
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			boxColl.enabled = false;
		}
	}

	public void OnAnimationEnd() {
		Destroy(gameObject);
	}

	public void OnAnimationStart() {
		StartCoroutine("FadeSprite");
	}

	private IEnumerator FadeSprite() {
		float startTime = Time.time;
		float endTime = startTime + fadeTime;
		while (Time.time < endTime) {
			sr.color = new Color(1f, 1f, 1f, 1-((Time.time - startTime) / fadeTime));
			yield return new WaitForEndOfFrame();
		}
	}

}
