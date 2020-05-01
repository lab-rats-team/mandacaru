using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public int hp;
	public Vector2 knockbackMultiplier;
	public float dyingAnimDuration;
	public float dazedTime;
	public MonoBehaviour movementScript;

	protected Animator anim;
	protected Rigidbody2D rb;
	protected float dazedRemainingTime;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
		if (dazedRemainingTime <= 0) {
			if (movementScript && !movementScript.enabled)
				movementScript.enabled = true;
		} else {
			dazedRemainingTime -= Time.deltaTime;
		}
	}

	public void TakeDamage(int damage, Vector2 knockback) {
		if (movementScript) {
			movementScript.enabled = false;
			dazedRemainingTime = dazedTime;
		}
		rb.AddForce(knockback * knockbackMultiplier, ForceMode2D.Impulse);
		hp -= damage;
		if (hp <= 0) {
			Debug.Log("Morreu");
			anim.SetTrigger("Die");
			Destroy(gameObject, dyingAnimDuration);
		}
	}

}
