using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour {

    // Atributos alteráveis pelo Unity
    public bool debugMode;
    public Collider2D playerCollider;
	public KeyCode jumpKey;
    [Range(0, 20)]   public float jumpUpVelocity;
    [Range(0,  1)]	 public float jumpRequestDuration;
    [Range(0, 15)]   public float floatyFallMultiplier;
    [Range(0, 15)]   public float fallMultiplier;

    // Atributos que recebem valor dinamicamente
    private LayerMask groundLayerIndex;
    private Rigidbody2D rb;
    private Animator animator;

    // Atributos de controle
    private bool jumpRequest;
    private float jumpRequestTimer;
	private bool holdingJump;
	private const float EXTRA_HEIGHT = 0.05f;

    // Chamado ao instanciar o Player
    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundLayerIndex = LayerMask.NameToLayer("Foreground");
	}
    
    // Chamado todo frame
    void Update() {
        if (Input.GetKeyDown(jumpKey)) {
            jumpRequest = true;
            jumpRequestTimer = jumpRequestDuration;
            if (debugMode) Debug.Log("Jump");
        }
        if (jumpRequest) {
            jumpRequestTimer -= Time.deltaTime;
            if (jumpRequestTimer <= 0) {
                jumpRequest = false;
                if (debugMode) Debug.Log("Timer end");
            }
        }

		holdingJump = Input.GetKey(jumpKey);

		animator.SetFloat("ySpeed", rb.velocity.y);

		if (debugMode) {
            Vector2 extraH = new Vector2(0f, EXTRA_HEIGHT);
            Debug.DrawLine(playerCollider.bounds.min, new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y), new Color(255, 2, 2));
            Debug.DrawLine(playerCollider.bounds.min, (Vector2)playerCollider.bounds.min - extraH);
            Debug.DrawLine(new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y), new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y) - extraH);
            Debug.DrawLine((Vector2)playerCollider.bounds.min - extraH, new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.min.y) - extraH, new Color(255, 2, 2));
        }
        
    }

    // Chamado 0 ou mais vezes por frame, sempre que necessário lidar com física
    void FixedUpdate() {

        if (jumpRequest && IsGrounded()) {
            rb.velocity -= new Vector2(0, rb.velocity.y);
            rb.velocity += Vector2.up * jumpUpVelocity;
            jumpRequest = false;
            if (debugMode) Debug.Log("Jumped");
        }

		if (rb.velocity.y < 0 && holdingJump) {
			rb.velocity += Vector2.up * Physics2D.gravity.y * (floatyFallMultiplier - 1) * Time.deltaTime;
		} else if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !holdingJump) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    public bool IsGrounded() {
        Collider2D collider = Physics2D.OverlapArea(playerCollider.bounds.min + new Vector3(0.1f, 0f, 0f),
                new Vector2(playerCollider.bounds.max.x - 0.1f, playerCollider.bounds.min.y - EXTRA_HEIGHT), 1 << groundLayerIndex);

        return collider != null;
    }
}
