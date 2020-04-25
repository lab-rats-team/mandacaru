using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Security.Cryptography;

public class MoleBehaviour : MonoBehaviour {

	public float regularSpeed;
	public float attackSpeedMultiplier;
	public float attackDistance;
	public Animator anim;
	public Vector2 regularSizeBox = new Vector2(0.24f, 0.48f);
	public Vector2 regularOffsetBox = new Vector2(0f, 0f);
	public Vector2 attackSizeBox = new Vector2(0.5f, 0.25f);
	public Vector2 attackOffsetBox = new Vector2(0f, 0f);

	private Rigidbody2D rb;
	private Transform transf;
	private Transform player;
	private BoxCollider2D boxColl;
	private LayerMask groundLayerIndex;
	private float speed;
	private bool attacking;

	
	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		transf = GetComponent<Transform>();
		player = GameObject.Find("Player").transform;
		boxColl = GetComponent<BoxCollider2D>();
		groundLayerIndex = LayerMask.NameToLayer("Foreground");

		speed = regularSpeed;
	}

    // Update is called once per frame
    void Update() {

		if (avistouPlayer() && !attacking) {
			speed *= attackSpeedMultiplier;
			boxColl.size = attackSizeBox;
			Debug.Log(boxColl.size);
			boxColl.offset = attackOffsetBox;
			attacking = true;
		}

		if (attacking) {
			float diff = player.position.x - transf.position.x;
			if (diff / speed < 0) speed = -speed; // Faz toupeira seguir player
		}

		if (reachBorder() || reachWall() && !attacking) {
			speed = -speed;
		}

		//Animação
		anim.SetBool("Run", attacking);
	}

	private void FixedUpdate() {
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}

	private bool avistouPlayer() {
		float xDiff = transf.position.x - player.position.x,
			  yDiff = transf.position.y - player.position.y;
		return Math.Sqrt(xDiff*xDiff + yDiff*yDiff) < attackDistance;
	}

	private bool reachBorder() {
		Vector2 leftBottom = boxColl.bounds.min;
		Vector2 rightBottom = new Vector2(boxColl.bounds.max.x, boxColl.bounds.min.y);
		//Debug.DrawLine(leftBottom, leftBottom + Vector2.down * 0.2f);
		//Debug.DrawLine(rightBottom, rightBottom + Vector2.down * 0.2f);
		return !Physics2D.Raycast(leftBottom, Vector2.down, 0.2f, 1 << groundLayerIndex)
			&& !Physics2D.Raycast(rightBottom, Vector2.down, 0.2f, 1 << groundLayerIndex);
	}

	private bool reachWall() {
		Vector2 rightOrigin = new Vector2(boxColl.bounds.max.x, boxColl.bounds.center.y);
		Vector2 leftOrigin = new Vector2(boxColl.bounds.min.x, boxColl.bounds.center.y);
		//Debug.DrawLine(leftOrigin, leftOrigin + Vector2.left * 0.2f);
		//Debug.DrawLine(rightOrigin, rightOrigin + Vector2.right * 0.2f);
		return Physics2D.Raycast(leftOrigin, Vector2.left, 0.2f, 1 << groundLayerIndex)
			|| Physics2D.Raycast(rightOrigin, Vector2.right, 0.2f, 1 << groundLayerIndex);
	}

}
