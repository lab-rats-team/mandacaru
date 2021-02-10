using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class StalactiteSpawner : MonoBehaviour
{
	public GameObject estalactite1Prefab;
	public GameObject estalactite2Prefab;
	public GameObject estalactite3Prefab;
	public GameObject estalactite4Prefab;

	public Transform estalactite1Transform;
	public Transform estalactite2Transform;
	public Transform estalactite3Transform;
	public Transform estalactite4Transform;

	public float distanciaGeracaoX = 5.1f;
	public float distanciaGeracaoY = 5.1f;

	private GameObject tempestalactite1Prefab;
	private GameObject tempestalactite2Prefab;
	private GameObject tempestalactite3Prefab;
	private GameObject tempestalactite4Prefab;
	private Transform player;
	private Transform transf;
	private bool destroyed = true;
	private Animator anim;

	//public ShakeCamera shake;

	// Start is called before the first frame update
	void Start()  {
		player = GameObject.FindWithTag("Player").transform;
		transf = GetComponent<Transform>();
		anim = GetComponent<Animator>();
		//ShakeCamera shake = gameObject.GetComponent<ShakeCamera>();
	}

    // Update is called once per frame
    void Update() {
		if (Distance(transf.position.x, player.position.x) < distanciaGeracaoX && Distance(transf.position.y, player.position.y) < distanciaGeracaoY && destroyed)	{
			anim.SetTrigger("Cai");
			
			///tremedeira
			//shake.TriggerShake();

			tempestalactite1Prefab = Instantiate(estalactite1Prefab, estalactite1Transform.position, estalactite1Transform.rotation) as GameObject;
			tempestalactite1Prefab.transform.parent = transf;
				
			tempestalactite2Prefab = Instantiate(estalactite2Prefab, estalactite2Transform.position, estalactite2Transform.rotation) as GameObject;
			tempestalactite2Prefab.transform.parent = transf;
		
			tempestalactite3Prefab = Instantiate(estalactite3Prefab, estalactite3Transform.position, estalactite3Transform.rotation) as GameObject;
			tempestalactite3Prefab.transform.parent = transf;
			
			tempestalactite4Prefab = Instantiate(estalactite4Prefab, estalactite4Transform.position, estalactite4Transform.rotation) as GameObject;
			tempestalactite4Prefab.transform.parent = transf;
		
			destroyed = false;
		}

	}
	private float Distance(float x, float y){
		return Math.Abs(x - y);
    }
}
