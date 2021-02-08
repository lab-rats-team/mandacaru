using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour {

	public string[] collideWithTags;

	private void OnTriggerEnter2D(Collider2D collision) {
		foreach (string tag in collideWithTags) {
			if (collision.CompareTag(tag))
				Destroy(gameObject);
		}
	}

}
