using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Transform player;
	private Transform transf;
    private GameObject tutorial;
    public float distanciaTransparencia = 0.5f;

    private float startTime; 
	private float time;

    private float tempo;//tempo em segundos 

    public float divisorOpacidade = 600f;
    private float opacidadeControle = 1.0f;
    private SpriteRenderer sprite;	

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
		transf = GetComponent<Transform>();
        tutorial = GetComponent<GameObject>();
        sprite = this.GetComponent<SpriteRenderer>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(transf.position.x + distanciaTransparencia <= player.position.x){
			tempo = Time.time - startTime;
            opacidadeControle -= (float)Math.Round(tempo)/divisorOpacidade;
            sprite.color = new Color(1f, 1f, 1f, opacidadeControle);
		}
    } 
}
