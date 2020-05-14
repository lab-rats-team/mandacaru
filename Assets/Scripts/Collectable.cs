using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public RuntimeAnimatorController jemmyAnimatorController;
	public MonoBehaviour component;
	public float delay = 1f;

	private SpriteRenderer sr;
	private float remainingDelay;
	private GameObject player;
	private MonoBehaviour[] scripts;
	Rigidbody2D playerRb;
	private float initialXPos;

	void Awake() {
		sr = GetComponent<SpriteRenderer>();
		component.enabled = false;
		remainingDelay = 0f;
		player = GameObject.FindGameObjectWithTag("Player");
		scripts = new MonoBehaviour[3];
		scripts[0] = player.GetComponent<JumpScript>();
		scripts[1] = player.GetComponent<CrouchScript>();
		scripts[2] = player.GetComponent<PlayerMovement>();
	}

	void Update() {
		if (remainingDelay > 0) {
			remainingDelay -= Time.deltaTime;
			player.transform.position = new Vector3(initialXPos, player.transform.position.y, player.transform.position.z);
			if (remainingDelay <= 0) {
				player.AddComponent(component.GetType());
				foreach (MonoBehaviour script in scripts) {
					script.enabled = true;
				}
				Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			Animator playerAnimator = collision.gameObject.GetComponent<Animator>();
			playerAnimator.runtimeAnimatorController = jemmyAnimatorController;
			sr.forceRenderingOff = true;
			foreach (MonoBehaviour script in scripts) {
				script.enabled = false;
			}
			playerRb = player.GetComponent<Rigidbody2D>();
			playerRb.velocity = new Vector2(0f, playerRb.velocity.y);
			initialXPos = player.transform.position.x;
			remainingDelay = delay;
		}
	}

}
