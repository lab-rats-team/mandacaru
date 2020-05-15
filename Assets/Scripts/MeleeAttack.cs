using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

	public float cooldown = 0.4f;
	public int damage = 20;
	public Vector2 knockback = new Vector2(1f, 1f);
	public float attackDelay = 0.14f;
	public KeyCode attackKey = KeyCode.Mouse0;

	private Transform attackPoint;
	private Transform upAttackPoint;
	private float radius = 0.28f;
	private float upRadius = 0.2f;

	private Transform player;
	private Animator anim;
	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private MonoBehaviour movementScript;
	private new Collider2D collider;
	private LayerMask enemyLayer;
	private Vector2 knockbackDirection;
	private float remainingCooldown;
	private float xDistance;
	private float yDistance;

	private float attackRemainingDelay;
	private bool attackRequest;

	private float upAttackRemainingDelay;
	private bool upAttackRequest;

	private void Start() {
		player = GetComponent<Transform>();
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		movementScript = GetComponent<PlayerMovement>();
		if (!player || !anim || !sr || !rb || !collider || !movementScript) Debug.Log("AAAAAA");
		attackPoint = GameObject.Find("attack_point").transform;
		upAttackPoint = GameObject.Find("up_attack_point").transform;
		enemyLayer = LayerMask.NameToLayer("Enemies");
		xDistance = attackPoint.position.x - player.position.x;
		yDistance = attackPoint.position.y - player.position.y;
		attackRequest = false;
	}

	// Update is called once per frame
	void Update() {

		if (sr.flipX) {
			attackPoint.position = player.position + Vector3.left*xDistance + Vector3.up*yDistance;
		} else {
			attackPoint.position = player.position + Vector3.right*xDistance + Vector3.up*yDistance;
		}

		if (remainingCooldown <= 0) {

			if (Input.GetKeyDown(attackKey)) {
				if (anim.GetBool("lookingUp")) {
					upAttackRequest = true;
					upAttackRemainingDelay = attackDelay;
					anim.SetTrigger("upAttack");
					knockbackDirection.Set(0.0f, knockback.y);
				} else {
					attackRequest = true;
					attackRemainingDelay = attackDelay;
					anim.SetTrigger("attack");
					bool isForLeft = attackPoint.position.x - player.position.x < 0;
					knockbackDirection.Set(isForLeft ? -knockback.x : knockback.x, knockback.y);
					rb.velocity = Vector2.zero;
				}
				remainingCooldown = cooldown;
				movementScript.enabled = false;
			}
		} else {
			remainingCooldown -= Time.deltaTime;
		}

	}

	private void FixedUpdate() {

		if (attackRequest) {
			if (attackRemainingDelay <= 0) {
				Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, radius, 1 << enemyLayer);
				foreach (Collider2D enemy in enemiesHit) {
					enemy.gameObject.GetComponent<Damageable>().TakeDamage(damage, knockbackDirection, collider);
				}
				attackRequest = false;
				movementScript.enabled = true;
			} else {
				attackRemainingDelay -= Time.deltaTime;
			}
		}

		if (upAttackRequest) {
			if (attackRemainingDelay <= 0) {
				Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(upAttackPoint.position, upRadius, 1 << enemyLayer);
				foreach (Collider2D enemy in enemiesHit) {
					enemy.gameObject.GetComponent<Damageable>().TakeDamage(damage, knockbackDirection, collider);
				}
				attackRequest = false;
				movementScript.enabled = true;
			} else {
				attackRemainingDelay -= Time.deltaTime;
			}
		}

	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPoint.position, radius);
		Gizmos.DrawWireSphere(upAttackPoint.position, upRadius);
	}

}
