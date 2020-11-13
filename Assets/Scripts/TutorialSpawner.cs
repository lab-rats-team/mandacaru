using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
   public GameObject tutorialPrefab;
	public float distanciaMaxima = 15.2f;

	private GameObject tempPrefab;
	private Transform player;
	private Transform transf;
	//private bool destroyed = true;

	// Start is called before the first frame update
	void Start() {
		player = GameObject.FindWithTag("Player").transform;
		transf = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update(){

		
			tempPrefab = Instantiate (inimigoPrefab) as GameObject;
			tempPrefab.transform.parent = transf;
			tempPrefab.transform.position = new Vector3(transf.position.x, transf.position.y, transf.position.z);
			//destroyed = false;
		

		if(tempPrefab) {
			if (Vector3.Distance(tempPrefab.transform.position, player.position) >= distanciaMaxima) {
				Destroy(tempPrefab);
				//destroyed = true;
			}
		}
	}
}
