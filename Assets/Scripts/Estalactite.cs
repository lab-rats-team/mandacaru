using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estalactite : MonoBehaviour
{
	private Transform transform;
	private Rigidbody2D rb;
	private Animator anim;

	//private float pos;
	//public bool fixa = false;

	
	void Start(){
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		transform = GetComponent<Transform>();
		//pos = transform.position.y;
	}

    void Update(){

		/*if (fixa){
			 transform.position = new Vector2(transform.position.x, pos);

		 }*/

	}

	void OnTriggerEnter2D(Collider2D col){
        if (col.tag == "Ground")
        {
			Debug.Log("bateuuuu");
			anim.SetTrigger("Destroi");
		}
	}
}
