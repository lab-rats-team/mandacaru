using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour {
	private Animator anim;
	private Rigidbody2D rb;
	private BoxCollider2D coll;

	private float dyingAnimDuration = 0.5f;

	void Start() {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Ground")||col.CompareTag("Player")) {
			StartCoroutine("Break");
		}
	}
	
	private IEnumerator Break() {
		anim.SetTrigger("Destroi");
		coll.enabled = false;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		yield return new WaitForSeconds(dyingAnimDuration);
		Destroy(gameObject);
	}
}
