using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {
	
	public GameObject player;
	public GameObject tutoPrefab;
	public Fader screenFader;
	public PaperPieceManager pieceManager;

	private bool allowed;
	private GameObject tutoInstance;

	void Start() {
		allowed = false;
	}

    void OnTriggerEnter2D(Collider2D other) {
		tutoInstance = Instantiate(tutoPrefab) as GameObject;
		tutoInstance.transform.position = transform.position;
		allowed = true;
    }
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.W) 
					&& Mathf.Abs(transform.position.x - player.transform.position.x) < .6f
					&& allowed) {
				int scene = SceneManager.GetActiveScene().buildIndex + (pieceManager.IsCompleted() ? 1 : 2);
				screenFader.FadeToScene(scene);
		}
	}
}
