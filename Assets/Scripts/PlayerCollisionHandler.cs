using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {

	public int damage;
	public Vector2 defaultKnockback;

	private Damageable damageScript;
	private SpriteRenderer sr;

	void Start() {
		damageScript = GetComponent<Damageable>();
		sr = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log(collider.gameObject.name);
		if (collider.gameObject.CompareTag("Projectile")) {
			float xDirection = sr.flipX ? 1f : -1f;
			damageScript.TakeDamage(damage, new Vector2(xDirection * defaultKnockback.x, defaultKnockback.y), collider);

		} else if (collider.gameObject.CompareTag("Enemy")) {
			float horDist = transform.position.x - collider.gameObject.transform.position.x;
			Vector2 knockback = new Vector2(horDist > 0 ? defaultKnockback.x : -defaultKnockback.x, defaultKnockback.y);
			damageScript.TakeDamage(damage, knockback, collider.gameObject.GetComponent<BoxCollider2D>());
		}
	}

}
