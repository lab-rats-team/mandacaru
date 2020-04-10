using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour {

	private BoxCollider2D boxColl;
	private JumpScript jumpScript;
	private PlayerMovement movementScript;
	private Rigidbody2D rb;
	private Animator animator;

	private static readonly Vector2 DEFAULT_COLLIDER_SIZE = new Vector2(0.35f, 0.65f);
	private static readonly Vector2 DEFAULT_COLLIDER_OFFSET = new Vector2(0.0f, 0.33f);

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
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update(){
		if (Input.GetKeyDown(KeyCode.S) && jumpScript.IsGrounded()) {
			rb.velocity = new Vector2(0.0f, rb.velocity.y);
			boxColl.size = new Vector2(sizeBoxX, sizeBoxY);
			boxColl.offset = new Vector2(offsetBoxX, offsetBoxY);
			jumpScript.enabled = false;
			movementScript.enabled = false;
			animator.SetBool("crouching", true);
		}
		else if (Input.GetKeyUp(KeyCode.S)) {
			boxColl.size = DEFAULT_COLLIDER_SIZE;
			boxColl.offset = DEFAULT_COLLIDER_OFFSET;
			jumpScript.enabled = true;
			movementScript.enabled = true;
			animator.SetBool("crouching", false);
		}
	}

}
