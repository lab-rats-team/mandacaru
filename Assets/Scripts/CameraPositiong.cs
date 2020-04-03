using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositiong : MonoBehaviour {
	
	public Transform background, cam, player;

    // Update is called once per frame
    void Update() {
        background.position = new Vector3(player.position.x, player.position.y, background.position.z);
        cam.position = new Vector3(player.position.x, player.position.y, cam.position.z);
    }

}
