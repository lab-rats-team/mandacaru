using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {

	public float cooldown;
	public int damage;
	public Transform attackPoint;
	public float radius;
	public Vector2 knockback;
	public float attackDelay;
	public KeyCode attackKey = KeyCode.Mouse1;
	public MonoBehaviour movementScript;

	private Transform player;
	private Animator anim;
	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private LayerMask enemyLayer;
	private Vector2 knockbackDirection;
	private float remainingCooldown;
	private float xDistance;
	private float yDistance;
	private float attackRemainingDelay;
	private bool attackRequest;

    // Start is called before the first frame update
    void Start() {
		player = GetComponent<Transform>();
		anim = GetComponent<Animator>();
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
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
				attackRequest = true;
				attackRemainingDelay = attackDelay;
				anim.SetTrigger("attack");
				bool isForLeft = attackPoint.position.x - player.position.x < 0;
				knockbackDirection.Set(isForLeft ? -knockback.x : knockback.x, knockback.y);
				remainingCooldown = cooldown;
				movementScript.enabled = false;
				rb.velocity = Vector2.zero;
			}
		} else {
			remainingCooldown -= Time.deltaTime;
		}

		if (attackRequest) {
			if (attackRemainingDelay <= 0) {
				Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, radius, 1 << enemyLayer);
				foreach (Collider2D enemy in enemiesHit) {
					enemy.gameObject.GetComponent<Damageable>().TakeDamage(damage, knockbackDirection);
				}
				attackRequest = false;
				movementScript.enabled = true;
			} else {
				attackRemainingDelay -= Time.deltaTime;
			}
		}

    }

}
