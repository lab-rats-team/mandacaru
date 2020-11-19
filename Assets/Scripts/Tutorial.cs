using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Transform player;
	private Transform transf;
    private GameObject tutorial;

    private float startTime; 
	private float time;
    private float tempo;

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
        if(transf.position.x <= player.position.x){
			tempo = Time.time - startTime;
            opacidadeControle -= (float)Math.Round(tempo)/6666f;
            sprite.color = new Color(1f, 1f, 1f, opacidadeControle);
		}
    } 
}
