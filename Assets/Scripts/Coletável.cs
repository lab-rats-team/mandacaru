using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletável : MonoBehaviour
{
	private BoxCollider2D hitbox;
	private int counter = 0;

	public int total = 4;

	

    // Start is called before the first frame update
    void Start()
    {
		hitbox = gameObject.GetComponent<BoxCollider2D>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision){
		if (collision.gameObject.CompareTag("Player")){
			Destroy(gameObject, 0.001f);
		}
	}
}
