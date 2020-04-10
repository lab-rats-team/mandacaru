using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchScript : MonoBehaviour {

	private JumpScript jumpScript;
	private PlayerMovement movementScript;
	private BoxCollider2D boxColl;
	private Rigidbody2D rb;

	private static Vector2 DEFAULT_COLLIDER_SIZE = new Vector2(1.174623f, 2.741882f);
	private static Vector2 DEFAULT_COLLIDER_OFFSET = new Vector2(-0.01909447f, 0.02948105f);

	public float sizeBoxX = 1.02f;
	public float sizeBoxY = 1.17f;
	public float offsetBoxX = 0f;
	public float offsetBoxY = -0.76f;

	// Start is called before the first frame update
	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		boxColl = GetComponent<BoxCollider2D>();
		jumpScript = GetComponent<JumpScript>();
		movementScript = GetComponent<PlayerMovement>();
	}

    // Update is called once per frame
    void Update(){
		if (Input.GetKeyDown(KeyCode.S) && jumpScript.IsGrounded()) {
			rb.velocity = new Vector2(0.0f, rb.velocity.y);
			boxColl.size = new Vector2(sizeBoxX, sizeBoxY);
			boxColl.offset = new Vector2(offsetBoxX, offsetBoxY);
			jumpScript.enabled = false;
			movementScript.enabled = false;
		}
		else if (Input.GetKeyUp(KeyCode.S)) {
			boxColl.size = DEFAULT_COLLIDER_SIZE;
			boxColl.offset = DEFAULT_COLLIDER_OFFSET;
			jumpScript.enabled = true;
			movementScript.enabled = true;
		}
	}

}
