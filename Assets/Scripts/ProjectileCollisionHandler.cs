using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour {

	public string[] collideWithTags;
	public Vector2 knockback;

	private float originX;

	void Start() {
		originX = transform.position.x;
	}

	private void OnTriggerEnter2D(Collider2D collision) {

		foreach (string tag in collideWithTags) {
			if (collision.CompareTag(tag)) {
				Damageable d = collision.gameObject.GetComponent<Damageable>();
				if (d != null) {
					// Testa se está indo pra esquerda, pra dar o knockback também pra esquerda
					if (transform.position.x < originX) knockback *= new Vector2(-1f, 0f);
					d.TakeDamage(1, knockback, gameObject.GetComponent<CircleCollider2D>());
				}
				Destroy(gameObject);
			}
		}
	}

}
