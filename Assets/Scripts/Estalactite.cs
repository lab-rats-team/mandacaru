using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estalactite : MonoBehaviour
{
	private Transform transform;
	private Rigidbody2D rb;
	private Animator anim;

	private float dyingAnimDuration = 0.5f;


	void Start(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		transform = GetComponent<Transform>();
	}

    void Update(){
	}

	private IEnumerator OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Ground" || col.tag == "Player"){

			anim.SetTrigger("Destroi");
			yield return new WaitForSeconds(dyingAnimDuration);
			Destroy(gameObject);
		}
	}
}
