using System.Collections;
using UnityEngine;

public class PaperPieceManager : MonoBehaviour {

	public GameObject[] papers;
	public float onScreenX;
	public float offScreenX;
	public float animationTime;
	public float timeShown;
	
	private bool slidingIn;
	private bool slidingOff;
	private float startTime;
	private RectTransform tr;

	void Awake() {
		tr = GetComponent<RectTransform>();
		startTime = -1f;
	}

	void Start() {
		tr.position = new Vector2(offScreenX, tr.position.y);
		for (int i = 0; i < papers.Length; i++) {
			if (SaveLoader.instance.IsCollectableCollected(i))
				papers[i].SetActive(true);
		}
	}

	void SlideOff() {
		startTime = Time.time;
		slidingOff = true;
		slidingIn = false;
	}

	void SlideIn() {
		startTime = Time.time;
		slidingOff = false;
		slidingIn = true;
	}

	void Update() {
		float timePassed = (Time.time - startTime);
		if (startTime == -1f || timePassed > animationTime) return;

		float x = 0f;
		float t = timePassed / animationTime;
		if (slidingIn) {
			x = offScreenX + t * (onScreenX - offScreenX);
		} else if (slidingOff) {
			x = onScreenX - t * (onScreenX - offScreenX);
		}
		Vector3 p = tr.position;
		tr.position = new Vector2(x, p.y);
	}

	public void AddPiece(int idx) {
		papers[idx].SetActive(true);
		SlideIn();
		StartCoroutine("HideAgain");
	}

	private IEnumerator HideAgain() {
		yield return new WaitForSeconds(timeShown);
		SlideOff();
	}
}
