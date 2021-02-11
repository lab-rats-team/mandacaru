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

	public void SlideOff() {
		startTime = Time.unscaledTime;
		slidingOff = true;
		slidingIn = false;
	}

	public void SlideIn() {
		startTime = Time.unscaledTime;
		slidingOff = false;
		slidingIn = true;
	}

	void Update() {
		float timePassed = (Time.unscaledTime - startTime);
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

	public bool IsCompleted() {
			foreach (var paper in papers) {
				if (!paper.activeSelf)
					return false;
			}
			return true;
	}

	private IEnumerator HideAgain() {
		yield return new WaitForSeconds(timeShown);
		SlideOff();
	}
}
