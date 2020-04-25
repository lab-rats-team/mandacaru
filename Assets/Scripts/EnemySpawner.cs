using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject inimigoPrefab;
	public float distanciaGeracao = 10.2f;
	public float distanciaMaxima = 15.2f;

	private GameObject tempPrefab;
	private GameObject player;
	private Transform transf;
	private bool destroyed = true;

	// Start is called before the first frame update
	void Start() {
		player = GameObject.FindWithTag("Player");
		transf = GetComponent<Transform>();
	}

    // Update is called once per frame
    void Update(){

		if (distanceFromPlayer() < distanciaGeracao && destroyed) {
			tempPrefab = Instantiate (inimigoPrefab) as GameObject;
			tempPrefab.transform.parent = transf;
			tempPrefab.transform.position = new Vector3(transf.position.x, transf.position.y, transf.position.z);
			destroyed = false;
		}

		if (distanceFromPlayer() >= distanciaMaxima && !destroyed) {
			Destroy(tempPrefab);
			destroyed = true;
		}
	}

	private float distanceFromPlayer() {
		return Vector3.Distance(transf.position, player.transform.position);
	}
}
