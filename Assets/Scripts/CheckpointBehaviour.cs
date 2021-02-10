using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			GameObject p = other.gameObject;
			int hp = p.GetComponent<Damageable>().hp;
			Vector3 pos = transform.position;
			SaveLoader.instance.UpdateSave(pos.x, pos.y, hp);
		}
    }

}
