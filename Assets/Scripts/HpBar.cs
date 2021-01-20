using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

	public GameObject player;
	public GameObject healthPoint;
	public float distanceBetweenPoints;
	[HideInInspector]
	public List<GameObject> healthPoints = new List<GameObject>();

	private Damageable damageScript;
	private int health;
	private int startingHealth;

	[SerializeField]
	private Sprite filledHealthPoint;

	[SerializeField]
	private Sprite emptyHealthPoint;

    
	public void Start()	{
		damageScript = player.GetComponent<Damageable>();
		startingHealth = damageScript.hp;
		health = damageScript.hp;
		UpdateHealthPoints();
	}

	public void Update() {
		if (health > damageScript.hp)
			UpdateHealthPoints();
	}

	private void UpdateHealthPoints() {
		health = damageScript.hp;

		foreach (var hp in healthPoints) {
			Destroy(hp);
		}

		for (int i = 0; i < startingHealth; i++) {
			if (damageScript.hp <= i) {
				healthPoint.GetComponent<Image>().sprite = emptyHealthPoint;
			} else {
				healthPoint.GetComponent<Image>().sprite = filledHealthPoint;
			}

			
			float posicaoXVida = 31.79996f + (i*distanceBetweenPoints);
			GameObject hp = Instantiate(
				healthPoint,
				Vector3.zero,
				Quaternion.identity,
				this.transform
			);
			RectTransform r = hp.GetComponent<RectTransform>();
			r.anchoredPosition = new Vector2(31.79996f + (i*distanceBetweenPoints), -5f);
			healthPoints.Add(hp);
		}

	}
	
}
