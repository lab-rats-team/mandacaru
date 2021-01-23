using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour {

	public bool towardsLeft;
	public string[] collideWithTags;

	private void OnTriggerEnter2D(Collider2D collision) {
		Damageable d = collision.gameObject.GetComponent<Damageable>();
		if (d != null)
			d.TakeDamage(1, new Vector2(0f, 3f), gameObject.GetComponent<CircleCollider2D>());

		foreach (string tag in collideWithTags) {
			if (collision.CompareTag(tag))
				Destroy(gameObject);
		}
	}

}
