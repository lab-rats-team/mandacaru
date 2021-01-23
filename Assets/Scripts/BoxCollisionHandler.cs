using UnityEngine;

public class BoxCollisionHandler : MonoBehaviour {
	
	private Animator anim;
	private float clipLength;

	void Awake() {
		anim = GetComponent<Animator>();
		AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
		foreach(AnimationClip clip in clips) {
			if(clip.name.Equals("break"))
				clipLength = clip.length;
		}
		Debug.Log(clipLength);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("projectile")) {
			anim.SetTrigger("break");
			Destroy(other.gameObject);
			Destroy(gameObject, clipLength);
		}
	}

}
