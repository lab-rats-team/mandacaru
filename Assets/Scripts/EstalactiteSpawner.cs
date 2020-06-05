using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstalactiteSpawner : MonoBehaviour
{
	public GameObject estalactite1Prefab;
	public GameObject estalactite2Prefab;
	public GameObject estalactite3Prefab;
	public GameObject estalactite4Prefab;
	public float distanciaGeracao = 5.1f;

	private GameObject tempestalactite1Prefab;
	private GameObject tempestalactite2Prefab;
	private GameObject tempestalactite3Prefab;
	private GameObject tempestalactite4Prefab;
	private Transform player;
	private Transform transf;
	private bool destroyed = true;

	private Animator anim;

	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindWithTag("Player").transform;
		transf = GetComponent<Transform>();
		anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		if (Vector3.Distance(transf.position, player.position) < distanciaGeracao && destroyed)
		{
			anim.SetTrigger("Cai");

			tempestalactite1Prefab = Instantiate(estalactite1Prefab) as GameObject;
			tempestalactite1Prefab.transform.parent = transf;
			//tempestalactite1Prefab.transform.position = new Vector3(transf.position.x, transf.position.y, transf.position.z);
			
			tempestalactite2Prefab = Instantiate(estalactite2Prefab) as GameObject;
			tempestalactite2Prefab.transform.parent = transf;
			//tempestalactite2Prefab.transform.position = new Vector3(transf.position.x+1, transf.position.y, transf.position.z);

			tempestalactite3Prefab = Instantiate(estalactite3Prefab) as GameObject;
			tempestalactite3Prefab.transform.parent = transf;
			//tempestalactite3Prefab.transform.position = new Vector3(transf.position.x+2, transf.position.y, transf.position.z);

			tempestalactite4Prefab = Instantiate(estalactite4Prefab) as GameObject;
			tempestalactite4Prefab.transform.parent = transf;
			//tempestalactite4Prefab.transform.position = new Vector3(transf.position.x+3, transf.position.y, transf.position.z);

			destroyed = false;
		}

	}
}
