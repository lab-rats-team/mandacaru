using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {
	
	public GameObject player;
	public GameObject tutoPrefab;
	public Fader screenFader;

	private bool allowed;
	private GameObject tutoInstance;

	void Start() {
		allowed = false;
	}

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {
		tutoInstance = Instantiate(tutoPrefab) as GameObject;
		tutoInstance.transform.position = transform.position;
		allowed = true;
    }
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.W) 
					&& Mathf.Abs(transform.position.x - player.transform.position.x) < .6f
					&& allowed) {
				screenFader.FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
