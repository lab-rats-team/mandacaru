using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
	public GameObject player;
	[HideInInspector]
	public int vidaMax;

	private Damageable scriptDano;
	private int vidaAtual;
	[SerializeField]
	private Sprite vidaCheia;
	[SerializeField]
	private Sprite vidaVazia;
	[SerializeField]
	private GameObject vida;
	private List<GameObject> pontosDeVida = new List<GameObject>();
    // Start is called before the first frame update
    
	public void Awake()
	{
		scriptDano = player.GetComponent<Damageable>();
		vidaMax = scriptDano.hp;
	}

	public void Update()
	{
		vidaAtual = scriptDano.hp;
		AtualizaBarraDeVida(vidaMax,vidaAtual);
	}


	public void AtualizaBarraDeVida(int totalDeVidas, int vidaAtual)
	{
		ResetaLista();
		for (int i = 0; i < totalDeVidas; i++)
		{
			if (vidaAtual <= i)
			{
				vida.GetComponent<Image>().sprite = vidaVazia;
			}
			else
			{
				vida.GetComponent<Image>().sprite = vidaCheia;

			}

			var posicaoXVida = transform.position.x + (i*20);
			var go =Instantiate(vida,new Vector3(posicaoXVida,transform.position.y,0), Quaternion.identity, this.transform) ;
			pontosDeVida.Add(go);
		}
	}

	public void ResetaLista()
	{
		foreach (var pontoDeVida in pontosDeVida)
		{
			Destroy(pontoDeVida);
		}
	}

	public void PerdeVida(int dano)
	{
		vidaAtual -= dano;
	}
	
}
