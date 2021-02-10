using System.Collections;
using UnityEngine;

public class Weapon : Collectable {

	public RuntimeAnimatorController weaponAnimatorController;
	public MonoBehaviour component;
	public float delay = 1f;

	private GameObject player;
	private MonoBehaviour[] scripts;
	private Rigidbody2D playerRb;
	private Damageable damageable;
	private int newSize;

	protected override void Awake() {
		base.Awake();
		component.enabled = false;
		player = GameObject.FindGameObjectWithTag("Player");
		scripts = new MonoBehaviour[3];
		scripts[0] = player.GetComponent<JumpScript>();
		scripts[1] = player.GetComponent<CrouchScript>();
		scripts[2] = player.GetComponent<PlayerMovement>();
	}

	protected override void Start() {
		base.Start();
		if (SaveLoader.instance.IsCollectableCollected((int) base.collectableId)) {
			EquipPlayer(false);
			player.GetComponent<Animator>().SetTrigger("skip");
		}
	}

	private void EquipPlayer(bool playSfx) {
			Animator playerAnimator = player.GetComponent<Animator>();
			playerAnimator.runtimeAnimatorController = weaponAnimatorController;
			if (playSfx)
				AudioManager.instance.Play("pick-up");
			player.AddComponent(component.GetType());

			// Fazer "Damageable" reconhecer o novo script para desativá-lo ao tomar dano
			damageable = player.GetComponent<Damageable>();
			newSize = damageable.movementScripts.Length + 1;
			System.Array.Resize<MonoBehaviour>(ref damageable.movementScripts, newSize);
			damageable.movementScripts[newSize - 1] = (MonoBehaviour) player.GetComponent(component.GetType());
	}

	protected override void OnCollect(Collider2D collision) {
		base.OnCollect(collision);
		EquipPlayer(true);
		StartCoroutine(FreezePlayer(delay));
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
