using UnityEngine;

public class TempAutoDestroyer : MonoBehaviour {

	private Transform player;

	void Start() {
		player = GameObject.FindWithTag("Player").transform;
	}

    void Update() {
        if (player.position.x < -31f)
			Destroy(gameObject);
    }
}
