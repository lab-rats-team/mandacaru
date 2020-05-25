using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksCollisionHandler : MonoBehaviour
{

	public int breakPoint;
	public float fadeTime;

	private int currentSprite;
	private Damageable dmgScript;
	private Animator anim;
	private BoxCollider2D boxColl;
	private Rigidbody2D rb;

	public void Awake() {
		dmgScript = GetComponent<Damageable>();
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

	public void OnBreak() {
		StartCoroutine(dmgScript.FadeSprite());
	}

	public void OnAnimationEnd() {
		Destroy(gameObject);
	}

}
