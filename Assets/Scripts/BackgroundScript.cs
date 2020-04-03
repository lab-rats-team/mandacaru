using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

	public Transform cam;
	public Transform background;

    // Update is called once per frame
    void Update()
    {
		background.position = cam.position;
    }
}
