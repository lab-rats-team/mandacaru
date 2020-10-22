using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

	public float cooldown = 0.4f;
	public int damage = 20;
	public Vector2 knockback = new Vector2(1f, 1f);
	public float attackDelay = 0.14f;
	public float recoveryTime = 0.185f;
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
	private LayerMask damageableObjLayer;
	private Vector2 knockbackDirection;
	private float remainingCooldown;
	private float xDistance;
	private float yDistance;

	private float attackRemainingDelay;
	private bool attackRequest;

	private float upAttackRemainingDelay;
	private bool upAttackRequest;

	private float recoveryRemainingTime;

	private void Start() {
		player = GetComponent<Transform>();
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		movementScript = GetComponent<PlayerMovement>();
		
		attackPoint = GameObject.Find("attack_point").transform;
		upAttackPoint = GameObject.Find("up_attack_point").transform;
		enemyLayer = LayerMask.NameToLayer("Enemies");
		damageableObjLayer = LayerMask.NameToLayer("DamageableObjects");

		xDistance = attackPoint.position.x - player.position.x;
		yDistance = attackPoint.position.y - player.position.y;
		attackRequest = false;
	}

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
					recoveryRemainingTime = recoveryTime;
				}
				remainingCooldown = cooldown;
				movementScript.enabled = false;
			}

		} else {
			remainingCooldown -= Time.deltaTime;
		}

		if (recoveryRemainingTime > 0) {
			recoveryRemainingTime -= Time.deltaTime;
			rb.velocity = Vector2.zero;
		} else if (!movementScript.enabled) {
			movementScript.enabled = true;
		}

	}

	private void FixedUpdate() {

		if (attackRequest) {
			if (attackRemainingDelay <= 0) {
				Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, radius, (1 << enemyLayer) | (1 << damageableObjLayer));
				foreach (Collider2D enemy in enemiesHit) {
					enemy.gameObject.GetComponent<Damageable>().TakeDamage(damage, knockbackDirection, collider);
				}
				attackRequest = false;
				recoveryRemainingTime = recoveryTime;

			} else {
				attackRemainingDelay -= Time.deltaTime;
			}
		}

		if (upAttackRequest) {
			if (attackRemainingDelay <= 0) {
				Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(upAttackPoint.position, upRadius, (1 << enemyLayer) | (1 << damageableObjLayer));
				foreach (Collider2D enemy in enemiesHit) {
					enemy.gameObject.GetComponent<Damageable>().TakeDamage(damage, knockbackDirection, collider);
				}
				upAttackRequest = false;
				movementScript.enabled = true;
			} else {
				upAttackRemainingDelay -= Time.deltaTime;
			}
		}

	}

	void OnDisable() {
		attackRequest = false;
		upAttackRequest = false;
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPoint.position, radius);
		Gizmos.DrawWireSphere(upAttackPoint.position, upRadius);
	}

}
