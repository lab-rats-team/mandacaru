using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Specialized;

public class Spawn : MonoBehaviour
{
	public GameObject inimigoPrefab;
	private GameObject tempPrefab;
	private GameObject player;
	private Transform transf;

	private Vector3 posicao;
	private Vector3 posicaoPlayer;
	
	public float distanciaGeracao = 10.2f;
	public float distanciaMaxima = 15.2f;
	private bool destruido = true;


	// Start is called before the first frame update
	void Start(){
		player = GameObject.Find("Player");
		transf = GetComponent<Transform>();
		
	}
    // Update is called once per frame
    void Update(){

		posicao = transf.position;
		posicaoPlayer = player.transform.position;


		if (avistouPlayer() && destruido) {
			tempPrefab = Instantiate (inimigoPrefab) as GameObject;
			tempPrefab.transform.parent = transf;
			tempPrefab.transform.position = new Vector3(transf.position.x, transf.position.y, transf.position.z);
			destruido = false;
		}
		if (Math.Abs(Math.Abs(posicao.x) - Math.Abs(posicaoPlayer.x)) >= distanciaMaxima){
			Destroy(tempPrefab);
			destruido = true;
		}

	}
	private bool avistouPlayer()
	{
		return (Math.Abs(Math.Abs(posicao.x) - Math.Abs(posicaoPlayer.x)) < distanciaGeracao);

	}
}
