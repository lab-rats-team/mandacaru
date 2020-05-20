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
		if (collider.gameObject.CompareTag("Projectile")) {
			ContactPoint2D[] points = new ContactPoint2D[6];
			collider.GetContacts(points);
			float xDirection = points[0].normal.x;
			if (xDirection == 0f)
				xDirection = sr.flipX ? 1f : -1f;
			damageScript.TakeDamage(damage, new Vector2(xDirection * defaultKnockback.x, defaultKnockback.y), collider);

		} else if (collider.gameObject.CompareTag("Enemy")) {
			float horDist = gameObject.transform.position.x - transform.position.x;
			Vector2 knockback = new Vector2(horDist > 0 ? defaultKnockback.x : -defaultKnockback.x, defaultKnockback.y);
			damageScript.TakeDamage(damage, knockback, collider.gameObject.GetComponent<BoxCollider2D>());
		}
	}

}
