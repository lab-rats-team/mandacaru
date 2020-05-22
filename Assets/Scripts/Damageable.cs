using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public int hp;
	public Vector2 knockbackMultiplier;
	public float dyingAnimDuration;
	public float dazedTime;
	public MonoBehaviour[] movementScripts;
	public Collider2D[] colliders;
	public float drag;
	public float invulnerableDuration;
	public bool blink;
	public float blinkDuration;


	private LayerMask playerLayer;
	private LayerMask enemiesLayer;
	private Animator anim;
	private Rigidbody2D rb;
	private new SpriteRenderer renderer;
	private float dazedRemainingTime;
	private bool isDazed;
	private bool dying;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();
		playerLayer = LayerMask.NameToLayer("Player");
		enemiesLayer = LayerMask.NameToLayer("Enemies");
		isDazed = false;
		dying = false;
	}

	// Update is called once per frame
	void Update() {
		if (isDazed && !dying) {
			if (dazedRemainingTime <= 0) {
				if (movementScripts.Length > 0) {
					foreach (MonoBehaviour script in movementScripts) {
						script.enabled = true;
					}
					isDazed = false;
					rb.drag = 0f;
				}
			} else {
				dazedRemainingTime -= Time.deltaTime;
			}
		}
	}

	public void TakeDamage(int damage, Vector2 knockback, Collider2D collider) {
		if (movementScripts.Length > 0) {
			foreach (MonoBehaviour script in movementScripts) {
				script.enabled = false;
			}
			dazedRemainingTime = dazedTime;
			isDazed = true;
			StartCoroutine(MakeInvulnerable(invulnerableDuration));
		}

		anim.SetTrigger("damage");
		rb.velocity = Vector2.zero;
		rb.drag = drag;
		rb.AddForce(knockback * knockbackMultiplier, ForceMode2D.Impulse);
		hp -= damage;
		if (hp <= 0) {
			dying = true;
			StartCoroutine(Die(collider));
		}
	}

	private IEnumerator MakeInvulnerable(float time) {
		Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, true);
		if (blink)
			StartCoroutine(Blink());
		yield return new WaitForSeconds(time);
		Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, false);
	}
 
	private IEnumerator Blink() {
		float endTime = Time.time + invulnerableDuration;
		while (Time.time < endTime) {
			renderer.enabled = false;
			yield return new WaitForSeconds(blinkDuration);
			renderer.enabled = true;
			yield return new WaitForSeconds(blinkDuration * 2);
		}
	}

	private IEnumerator Die(Collider2D otherCollider) {
		foreach (Collider2D thisCollider in colliders) {
			Physics2D.IgnoreCollision(thisCollider, otherCollider, true);
		}
		anim.SetTrigger("Die");
		foreach (MonoBehaviour script in movementScripts) {
			script.enabled = false;
		}
		yield return new WaitForSeconds(dyingAnimDuration);
		Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, false);
		Destroy(gameObject);
	}

}
