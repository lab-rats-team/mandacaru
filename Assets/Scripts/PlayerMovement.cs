using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator animator;
	private float xVelocity;

	// Start is called before the first frame update
	void Awake() {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
		animator.SetFloat("xSpeed", xVelocity < 0 ? -xVelocity : xVelocity);
		xVelocity = Input.GetKey(KeyCode.A) ? -1 : (Input.GetKey(KeyCode.D) ? 1 : 0);
		sr.flipX = xVelocity < 0 || (sr.flipX && xVelocity == 0);
    }

	void FixedUpdate() {
		rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
	}

}
/*
eraMenorQueZero | eMenorQueZero
	 */
