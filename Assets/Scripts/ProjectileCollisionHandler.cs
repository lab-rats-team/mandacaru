using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour {

	public bool towardsLeft;
	public string[] collideWithTags;
	public Vector2 knockback;

	private void OnTriggerEnter2D(Collider2D collision) {

		foreach (string tag in collideWithTags) {
			if (collision.CompareTag(tag)) {
				Damageable d = collision.gameObject.GetComponent<Damageable>();
				if (d != null) {
					d.TakeDamage(1, knockback, gameObject.GetComponent<CircleCollider2D>());
				}
				Destroy(gameObject);
			}
		}
	}

}
