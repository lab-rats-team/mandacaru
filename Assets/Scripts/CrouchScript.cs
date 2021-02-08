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
	private Vector2 defaultColliderSize;
	private Vector2 defaultColliderOffset;
	private bool crouching = false;
	private bool dashing = false;

	public float dashForce;
	public float dashDuration;
	public float sizeBoxX = 0.3f;
	public float sizeBoxY = 0.45f;
	public float offsetBoxX = 0f;
	public float offsetBoxY = 0.23f;
	public Vector2 dashCollSize = new Vector2(0.5f, 0.4f);
	public Vector2 dashCollOffset = new Vector2(0.0227f, 0.196f);

	// Start is called before the first frame update
	void Awake() {
		boxColl = GetComponent<BoxCollider2D>();
		defaultColliderSize = boxColl.bounds.size;
		defaultColliderOffset = boxColl.offset;
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
		} else if (crouching && !Input.GetKey(KeyCode.S)) {
			boxColl.size = defaultColliderSize;
			boxColl.offset = defaultColliderOffset;
			SetCrouching(false);
		}
		
		if (crouching && Input.GetKeyDown(jumpScript.jumpKey)) {
			StartCoroutine("Dash");
		}
	}
	
	public void InterruptDash() {
		StopCoroutine("Dash");
		rb.velocity = Vector2.zero;
		dashing = false;
		animator.SetBool("dashing", false);
		SetCrouching(false);
		boxColl.size = defaultColliderSize;
		boxColl.offset = defaultColliderOffset;
	}
	
	private IEnumerator Dash() {
		rb.AddForce((sr.flipX ? Vector2.left : Vector2.right) * dashForce, ForceMode2D.Impulse);
		crouching = false;
		dashing = true;
		animator.SetBool("dashing", true);
		boxColl.size = dashCollSize;
		boxColl.offset = dashCollOffset;
		
		yield return new WaitForSeconds(dashDuration);
		InterruptDash();
	}

	private void SetCrouching(bool isCrouching) {
		crouching = isCrouching;
		jumpScript.enabled = movementScript.enabled = !crouching;
		animator.SetBool("crouching", crouching);
	}

}
