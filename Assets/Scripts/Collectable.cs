using System.Collections;
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

	void Start() {
		if (SaveLoader.instance.IsCollectableCollected((int) Stage1Collectable.jemmy)) {
			EquipPlayer(false);
			player.GetComponent<Animator>().SetTrigger("skip");
		}
	}

	private void EquipPlayer(bool playSfx) {
			Animator playerAnimator = player.GetComponent<Animator>();
			playerAnimator.runtimeAnimatorController = jemmyAnimatorController;
			sr.forceRenderingOff = true;
			if (playSfx)
				AudioManager.instance.Play("pick-up");
			SaveLoader.instance.SetCollectableCollected((int) Stage1Collectable.jemmy);
			player.AddComponent(component.GetType());

			// Fazer "Damageable" reconhecer o novo script para desativá-lo ao tomar dano
			damageable = player.GetComponent<Damageable>();
			newSize = damageable.movementScripts.Length + 1;
			System.Array.Resize<MonoBehaviour>(ref damageable.movementScripts, newSize);
			damageable.movementScripts[newSize - 1] = (MonoBehaviour) player.GetComponent(component.GetType());
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			EquipPlayer(true);
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

		foreach (MonoBehaviour script in scripts) {
			script.enabled = true;
		}
		playerRb.velocity = new Vector2(0f, -1f);
		playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
		Destroy(gameObject);
	}

}
