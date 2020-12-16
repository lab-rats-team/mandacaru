using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUIScript : MonoBehaviour
{

	public GameObject player;

	[HideInInspector]
	public int vidaMax;
	private Damageable scriptDano;


	[SerializeField]
	private GameObject UiLeft;

	[SerializeField]
	private GameObject UiMidle;

	[SerializeField]
	private GameObject UiRight;

	public float positionXMidle;
	public float positionXRight;
	public float DistancePositionsXMidle;
	public static readonly float RIGHT_MARGIN_X = 15.65f;

	// Start is called before the first frame update
	void Start()
    {
		scriptDano = player.GetComponent<Damageable>();
		vidaMax = scriptDano.hp;
		UiHpBars();
		
    }

    // Update is called once per frame
    void Update()
    {
     
    }

	public void UiHpBars()
	{

		Instantiate(UiLeft, new Vector3(transform.position.x,transform.position.y,0),Quaternion.identity, this.transform);
		float distUi = vidaMax - 3;
		if (vidaMax > 1)
		{
			Instantiate(UiMidle, new Vector3(transform.position.x + positionXMidle, transform.position.y, 0), Quaternion.identity, this.transform);

			if (distUi > 0)
			{
				for (int i = 0; i < distUi; i++)
				{

					Instantiate(UiMidle, new Vector3(transform.position.x + (positionXMidle + DistancePositionsXMidle * (i + 1)), transform.position.y, 0), Quaternion.identity, this.transform);

				}
			}
			Instantiate(UiRight, new Vector3(transform.position.x + (positionXRight + (RIGHT_MARGIN_X * distUi)), transform.position.y, 0), Quaternion.identity, this.transform);
		}
		else Instantiate(UiRight, new Vector3(transform.position.x + (positionXRight + (RIGHT_MARGIN_X  * distUi) + 5), transform.position.y, 0), Quaternion.identity, this.transform);
	}

}
