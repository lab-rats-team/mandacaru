using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollisionHandler : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D collider) {
		if (collider.CompareTag("Enemy")) {
			collider.gameObject.GetComponent<Damageable>().TakeDamage(999, Vector2.zero, collider);
		}
	}
}
