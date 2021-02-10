using UnityEngine;

public class Collectable : MonoBehaviour {

	public StageCollectable collectableId;

	private static readonly float fluctuationSpeed = 2f;
	private static readonly float yVariation = .2f;
	private SpriteRenderer sr;
	private float startY;

	protected virtual void Awake() {
		sr = GetComponent<SpriteRenderer>();
		startY = transform.position.y;
	}

	protected virtual void Start() {
		if (SaveLoader.instance.IsCollectableCollected((int) collectableId))
			Destroy(gameObject, .5f);
	}

	protected virtual void Update() {
		float y = Mathf.Sin(Time.time * fluctuationSpeed) * yVariation;
		Vector3 p = transform.position;
		transform.position = new Vector3(p.x, startY + y, p.z);
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
