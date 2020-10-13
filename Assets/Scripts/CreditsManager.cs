using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour {
	public GameObject credits;
	public float initialY;
	public float finalY;
	public float rollTime;
	
	private float currentY;

    void Update() {
		float interpolation = Time.time/rollTime;
		currentY = initialY + (finalY - initialY) * interpolation;
		
		Vector3 pos = credits.transform.position;
        credits.transform.position = new Vector3(pos.x, currentY, pos.z);
    }
}
