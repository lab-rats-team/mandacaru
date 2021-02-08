using UnityEngine;

public class Collectable : MonoBehaviour {

	public StageCollectable collectableId;

	protected SpriteRenderer sr;

	protected virtual void Awake() {
		sr = GetComponent<SpriteRenderer>();
	}

	protected virtual void Start() {
		Debug.Log(gameObject.name);
		if (SaveLoader.instance.IsCollectableCollected((int) collectableId))
			Destroy(gameObject, .5f);
		Debug.Log(gameObject.name);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag("Player"))
			OnCollect(coll);
	}

	protected virtual void OnCollect(Collider2D coll) {
		sr.forceRenderingOff = true;
		SaveLoader.instance.SetCollectableCollected((int) collectableId);
	}

}
