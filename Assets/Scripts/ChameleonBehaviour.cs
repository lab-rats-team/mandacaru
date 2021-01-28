using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonBehaviour : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed;
	public float fireRate;
	public Vector3 projectileOriginSpawn = new Vector3(-0.267f, -0.042f, 0f);
	public float initialDelay;

	private Animator anim;
	private SpriteRenderer sr;

	// Start is called before the first frame update
	void Start() {
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		if (sr.flipX) projectileOriginSpawn.x = -projectileOriginSpawn.x;
		InvokeRepeating("Shoot", initialDelay, fireRate);
	}

	private void Shoot() {
		anim.SetTrigger("shoot");
	}

	public void CreateProjectile() {
		GameObject projectileinstance = Instantiate(projectile, gameObject.transform.position + projectileOriginSpawn, Quaternion.identity);
		projectileinstance.GetComponent<Rigidbody2D>().velocity = projectileSpeed * (sr.flipX ? Vector2.right : Vector2.left);
	}

	public void RestoreHp() => GetComponent<Damageable>().hp = 100;

}
