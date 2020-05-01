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
	public Vector2 regularSizeBox = new Vector2(0.24f, 0.48f);
	public Vector2 regularOffsetBox = new Vector2(0f, 0f);
	public Vector2 attackSizeBox = new Vector2(0.5f, 0.25f);
	public Vector2 attackOffsetBox = new Vector2(0f, 0f);

	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private Animator anim;
	private Transform transf;
	private Transform player;
	private BoxCollider2D boxColl;
	private LayerMask groundLayerIndex;
	private float speed;
	private double pDistance;
	private bool attacking;

	
	// Start is called before the first frame update
	void Start() {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		transf = GetComponent<Transform>();
		player = GameObject.Find("Player").transform;
		boxColl = GetComponent<BoxCollider2D>();
		groundLayerIndex = LayerMask.NameToLayer("Foreground");
		sr = GetComponent<SpriteRenderer>();

		speed = regularSpeed;
	}

    // Update is called once per frame
    void Update() {

		pDistance = playerDistance();

		//if (pDistance > 

		if (pDistance < attackDistance && !attacking) {
			speed *= attackSpeedMultiplier;
			boxColl.size = attackSizeBox;
			boxColl.offset = attackOffsetBox;
			attacking = true;
		}

		if (attacking) {
			float diff = player.position.x - transf.position.x;
			if (diff / speed < 0) speed = -speed; // Faz toupeira seguir player
		}

		if ((reachBorder() || reachWall()) && !attacking) {
			speed = -speed;
		}

		sr.flipX = speed < 0;

		//Animação
		anim.SetBool("Run", attacking);

	}

	private void FixedUpdate() {
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}

	private double playerDistance() {
		float xDiff = transf.position.x - player.position.x,
			  yDiff = transf.position.y - player.position.y;
		return Math.Sqrt(xDiff*xDiff + yDiff*yDiff);
	}

	private bool reachBorder() {
		Vector2 leftBottom = boxColl.bounds.min;
		Vector2 rightBottom = new Vector2(boxColl.bounds.max.x, boxColl.bounds.min.y);
		if (speed < 0)
			return !Physics2D.Raycast(leftBottom, Vector2.down, 0.2f, 1 << groundLayerIndex);
		return !Physics2D.Raycast(rightBottom, Vector2.down, 0.2f, 1 << groundLayerIndex);
	}

	private bool reachWall() {
		Vector2 rightOrigin = new Vector2(boxColl.bounds.max.x, boxColl.bounds.center.y);
		Vector2 leftOrigin = new Vector2(boxColl.bounds.min.x, boxColl.bounds.center.y);
		if (speed < 0)
			return Physics2D.Raycast(leftOrigin, Vector2.left, 0.2f, 1 << groundLayerIndex);
		return Physics2D.Raycast(rightOrigin, Vector2.right, 0.2f, 1 << groundLayerIndex);
	}

}
