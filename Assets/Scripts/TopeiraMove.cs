using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Security.Cryptography;

public class TopeiraMove : MonoBehaviour{
	
	private Rigidbody2D rb;
	private Transform	transf;
	private GameObject player;

	public Vector2	 velocidade;
	public Vector2	velocidadeAumentada;

	private Vector3 posicao;
	private Vector3 posicaoPlayer;
	public float distanciaAtack;

	public Animator anime;
	private bool run;
	

	private BoxCollider2D boxColl;
	public float sizeBoxX = 0.5987514f;
	public float sizeBoxY = 0.3276178f;
	public float offsetBoxX = -0.02241823f;
	public float offsetBoxY = -0.002517678f;

	private Vector3 posicaoAnterior;
	private bool virado = false;

	
	// Start is called before the first frame update
	void Start(){
		rb = GetComponent<Rigidbody2D>();
		transf = GetComponent<Transform>();
		boxColl = GetComponent<BoxCollider2D>();
		player = GameObject.Find("Player");

		rb.velocity = velocidade;
		posicaoAnterior = transf.position;
		posicaoAnterior.x = 0;
	}

    // Update is called once per frame
    void Update(){
		
		posicao = transf.position;
		posicaoPlayer = player.transform.position;



		if (posicaoAnterior.x != posicao.x) {
			virado = false;
        }else{
			virado = true;
        }
		
		
		if (!virado){
			if (avistouPlayer())
			{
				rb.velocity = velocidadeAumentada;
				boxColl.size = new Vector2(sizeBoxX, sizeBoxY);
				boxColl.offset = new Vector2(offsetBoxX, offsetBoxY);
				run = true;
			}
			else
			{
				rb.velocity = velocidade;
				boxColl.size = new Vector2(0.297572f, 0.4825935f);
				boxColl.offset = new Vector2(0.008284271f, -0.006903768f);
				run = false;
			}
			
		}
		else if (virado){
			if (avistouPlayer())
			{
				rb.velocity = -velocidadeAumentada;
				boxColl.size = new Vector2(sizeBoxX, sizeBoxY);
				boxColl.offset = new Vector2(offsetBoxX, offsetBoxY);
				run = true;
			}
			else
			{
				rb.velocity = -velocidade;
				boxColl.size = new Vector2(0.297572f, 0.4825935f);
				boxColl.offset = new Vector2(0.008284271f, -0.006903768f);
				run = false;
			}	
		}
		posicaoAnterior.x = posicao.x;


		//Animação
		anime.SetBool("Run", run);
	}

	private bool avistouPlayer() {
		return (Math.Abs(Math.Abs(posicao.x) - Math.Abs(posicaoPlayer.x)) < distanciaAtack);

	}

}
