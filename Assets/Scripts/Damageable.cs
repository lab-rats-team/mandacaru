using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public int hp;
	public Vector2 knockbackMultiplier;
	public float dyingAnimDuration;
	public float dazedTime;
	public MonoBehaviour movementScript;
	public float invulnerableDuration;
	public Collider2D thisCollider;
	public int blinkTimes;
	public float blinkDuration;


	private LayerMask playerLayer;
	private LayerMask enemiesLayer;
	private Animator anim;
	private Rigidbody2D rb;
	private SpriteRenderer renderer;
	private float dazedRemainingTime;
	private float intangibleRemainingTime;
	private bool isDazed;

	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();
		playerLayer = LayerMask.NameToLayer("Player");
		enemiesLayer = LayerMask.NameToLayer("Enemies");
		isDazed = false;
	}

	// Update is called once per frame
	void Update() {
		if (dazedRemainingTime <= 0 && isDazed) {
			if (movementScript && !movementScript.enabled && isDazed) {
				movementScript.enabled = true;
				isDazed = false;
			}
		} else {
			dazedRemainingTime -= Time.deltaTime;
		}
	}

	public void TakeDamage(int damage, Vector2 knockback, Collider2D collider) {
		if (movementScript) {
			movementScript.enabled = false;
			dazedRemainingTime = dazedTime;
			isDazed = true;
			StartCoroutine(MakeInvulnerable(invulnerableDuration));
		}

		anim.SetTrigger("damage");
		rb.velocity = Vector2.zero;
		rb.AddForce(knockback * knockbackMultiplier, ForceMode2D.Impulse);
		hp -= damage;
		if (hp <= 0) {
			StartCoroutine(Die(collider));
		}
	}

	private IEnumerator MakeInvulnerable(float time) {
		Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, true);
		StartCoroutine(Blink());
		yield return new WaitForSeconds(time);
		Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, false);
	}
 
	private IEnumerator Blink() {
		for (int i = 0; i < blinkTimes; i++) {
			renderer.enabled = false;
			yield return new WaitForSeconds(blinkDuration);
			renderer.enabled = true;
			yield return new WaitForSeconds(blinkDuration*2);
		}
	}

	private IEnumerator Die(Collider2D collider) {
		Physics2D.IgnoreCollision(thisCollider, collider, true);
		Debug.Log(thisCollider.gameObject.name + " and " + collider.gameObject.name + " dont collide");
		anim.SetTrigger("Die");
		movementScript.enabled = false;
		yield return new WaitForSeconds(dyingAnimDuration);
		Destroy(gameObject, dyingAnimDuration);
	}

}
