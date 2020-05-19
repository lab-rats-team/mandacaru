using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

	public int damage;

	private Damageable dmg;
	private SpriteRenderer sr;

	void Start() {
		dmg = GetComponent<Damageable>();
		sr = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Enemy")) {
			ContactPoint2D[] points = new ContactPoint2D[6];
			collision.GetContacts(points);
			float xDirection = points[0].normal.x;
			if (xDirection == 0f)
				xDirection = sr.flipX ? 1f : -1f;
			dmg.TakeDamage(damage, /*(temp)*/ new Vector2(xDirection, 2f), collision);
		}
	}
}
