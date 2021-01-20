using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUIScript : MonoBehaviour {

	public GameObject player;
	public float middleMinX;

	private Damageable damageScript;
	private HpBar hpScript;

	[SerializeField]
	private GameObject UiLeftSquare;

	[SerializeField]
	private GameObject UiMiddleSquare;

	[SerializeField]
	private GameObject UiRightSquare;

	void Start() {
		hpScript = GetComponentInChildren<HpBar>();
		if (hpScript == null)
			Debug.LogError("Erro: o script Hp Bar não foi encontrado nos filhos de " + gameObject.name);
		damageScript = player.GetComponent<Damageable>();
		UiHpBars();
    }

	public void UiHpBars() {

		float healthPointsWidth = hpScript.distanceBetweenPoints*hpScript.healthPoints.Count;
		RectTransform middleRect = UiMiddleSquare.GetComponent<RectTransform>();
		RectTransform rightRect = UiRightSquare.GetComponent<RectTransform>();

		middleRect.sizeDelta = new Vector2(healthPointsWidth, middleRect.sizeDelta.y);
		middleRect.anchoredPosition =
			new Vector2(middleMinX + middleRect.sizeDelta.x/2, middleRect.anchoredPosition.y);
		rightRect.anchoredPosition =
			new Vector2(middleMinX + middleRect.sizeDelta.x, rightRect.anchoredPosition.y);

	}
}

