using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public int hp;
	public Vector2 knockbackMultiplier;
	public float dyingAnimDuration;
	public float dazedTime;
	public MonoBehaviour movementScript;
	public float intangibleTime;

	private LayerMask playerLayer;
	private LayerMask enemiesLayer;
	private Animator anim;
	private Rigidbody2D rb;
	private float dazedRemainingTime;
	private float intangibleRemainingTime;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		playerLayer = LayerMask.NameToLayer("Player");
		enemiesLayer = LayerMask.NameToLayer("Enemies");
	}

	// Update is called once per frame
	void Update() {
		if (dazedRemainingTime <= 0) {
			if (movementScript && !movementScript.enabled)
				movementScript.enabled = true;
		} else {
			dazedRemainingTime -= Time.deltaTime;
		}

		if (intangibleRemainingTime > 0) {
			intangibleRemainingTime -= Time.deltaTime;
			if (intangibleRemainingTime <= 0)
				Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, false);
		}
	}

	public void TakeDamage(int damage, Vector2 knockback) {
		if (movementScript) {
			movementScript.enabled = false;
			dazedRemainingTime = dazedTime;
		}
		Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, true);
		intangibleRemainingTime = intangibleTime;
		anim.SetTrigger("damage");
		rb.velocity = Vector2.zero;
		rb.AddForce(knockback * knockbackMultiplier, ForceMode2D.Impulse);
		hp -= damage;
		if (hp <= 0) {
			anim.SetTrigger("Die");
			movementScript.enabled = false;
			Destroy(gameObject, dyingAnimDuration);
		}
	}

}
