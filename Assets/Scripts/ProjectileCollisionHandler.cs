using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour {

	public string[] collideWithTags;

	private void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log(collision.tag);
		foreach (string tag in collideWithTags) {
			if (collision.CompareTag(tag))
				Destroy(gameObject);
		}
	}

}
