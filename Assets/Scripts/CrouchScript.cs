using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour {

	private BoxCollider2D boxColl;
	private JumpScript jumpScript;
	private PlayerMovement movementScript;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator animator;
	private bool crouching = false;
	private bool dashing = false;
	private float dashRemainingTime;

	private static readonly Vector2 DEFAULT_COLLIDER_SIZE = new Vector2(0.35f, 0.65f);
	private static readonly Vector2 DEFAULT_COLLIDER_OFFSET = new Vector2(0.0f, 0.33f);

	public float dashForce;
	public float dashDuration;
	public float sizeBoxX = 0.3f;
	public float sizeBoxY = 0.45f;
	public float offsetBoxX = 0f;
	public float offsetBoxY = 0.23f;

	// Start is called before the first frame update
	void Awake() {
		boxColl = GetComponent<BoxCollider2D>();
		jumpScript = GetComponent<JumpScript>();
		movementScript = GetComponent<PlayerMovement>();
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.S) && !crouching && jumpScript.IsGrounded() && !dashing) {
			rb.velocity = new Vector2(0.0f, rb.velocity.y);
			boxColl.size = new Vector2(sizeBoxX, sizeBoxY);
			boxColl.offset = new Vector2(offsetBoxX, offsetBoxY);
			SetCrouching(true);
		}
		else if (Input.GetKeyUp(KeyCode.S) && crouching) {
			boxColl.size = DEFAULT_COLLIDER_SIZE;
			boxColl.offset = DEFAULT_COLLIDER_OFFSET;
			SetCrouching(false);
		}

		if (crouching && Input.GetKeyDown(jumpScript.jumpKey)) {
			rb.AddForce((sr.flipX ? Vector2.left : Vector2.right) * dashForce, ForceMode2D.Impulse);
			dashRemainingTime = dashDuration;
			crouching = false;
			dashing = true;
			animator.SetBool("dashing", true);
		} else if (dashing) {
			dashRemainingTime -= Time.deltaTime;
			if (dashRemainingTime <= 0) {
				rb.velocity = new Vector2(0f, rb.velocity.y);
				dashing = false;
				animator.SetBool("dashing", false);
				boxColl.size = DEFAULT_COLLIDER_SIZE;
				boxColl.offset = DEFAULT_COLLIDER_OFFSET;
				SetCrouching(false);
			}
		}
	}

	private void SetCrouching(bool isCrouching) {
		crouching = isCrouching;
		jumpScript.enabled = !crouching;
		rb.constraints = crouching ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
		animator.SetBool("crouching", crouching);
	}

}
