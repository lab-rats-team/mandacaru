using System;
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
	private bool sumindo;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
		transf = GetComponent<Transform>();
        tutorial = GetComponent<GameObject>();
        sprite = this.GetComponent<SpriteRenderer>();
        startTime = Time.time;
		sumindo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transf.position.x + distanciaTransparencia <= player.position.x){
			sumindo = true;
		}

		if(sumindo) {
			tempo = Time.time - startTime;
            opacidadeControle -= (float)Math.Round(tempo)/divisorOpacidade;
            sprite.color = new Color(1f, 1f, 1f, opacidadeControle);
		}

		if(Vector3.Distance(player.transform.position, transform.position) > 10f)
			Destroy(gameObject);
    } 
}
