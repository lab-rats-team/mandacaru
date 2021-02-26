using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_shadow : MonoBehaviour
{
	
	// Start is called before the first frame update
	void Start()
	{
		Color shadowColor = new Color(0.5f, 0.5f, 0.5f, 1f);
		foreach (GameObject objeto in Object.FindObjectsOfType(typeof(GameObject)))
		{

			if (objeto.tag == "Shadow scenario")
			{
				objeto.GetComponent<SpriteRenderer>().color = shadowColor;
			}
		}

		
	}
}
