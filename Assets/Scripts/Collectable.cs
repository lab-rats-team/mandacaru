using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	public RuntimeAnimatorController jemmyAnimatorController;
	public MonoBehaviour component;
	public float delay = 1f;

	private SpriteRenderer sr;
	private GameObject player;
	private MonoBehaviour[] scripts;
	private Rigidbody2D playerRb;
	private Damageable damageable;
	private int newSize;

	void Awake() {
		sr = GetComponent<SpriteRenderer>();
		component.enabled = false;
		player = GameObject.FindGameObjectWithTag("Player");
		scripts = new MonoBehaviour[3];
		scripts[0] = player.GetComponent<JumpScript>();
		scripts[1] = player.GetComponent<CrouchScript>();
		scripts[2] = player.GetComponent<PlayerMovement>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			Animator playerAnimator = collision.gameObject.GetComponent<Animator>();
			playerAnimator.runtimeAnimatorController = jemmyAnimatorController;
			sr.forceRenderingOff = true;
			AudioManager.instance.Play("pick-up");

			// Fazer "Damageable" reconhecer o novo script para desativá-lo ao tomar dano
			damageable = collision.gameObject.GetComponent<Damageable>();
			newSize = damageable.movementScripts.Length + 1;
			System.Array.Resize<MonoBehaviour>(ref damageable.movementScripts, newSize);

			StartCoroutine(FreezePlayer(delay));
		}
	}

	private IEnumerator FreezePlayer(float time) {
		foreach (MonoBehaviour script in scripts) {
			script.enabled = false;
		}
		playerRb = player.GetComponent<Rigidbody2D>();
		playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

		yield return new WaitForSeconds(time);

		player.AddComponent(component.GetType());
		damageable.movementScripts[newSize - 1] = (MonoBehaviour) player.GetComponent(component.GetType());
		foreach (MonoBehaviour script in scripts) {
			script.enabled = true;
		}
		playerRb.velocity = new Vector2(0f, -1f);
		playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
		Destroy(gameObject);
	}

}
