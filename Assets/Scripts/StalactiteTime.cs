using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteTime : MonoBehaviour
{
	private Rigidbody2D rb;
    public float tempoEspera = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.gravityScale);
        StartCoroutine ("AlterarGravidade");
        Debug.Log(rb.gravityScale);
    }

    IEnumerator AlterarGravidade(){
        yield return new WaitForSeconds(tempoEspera);
        rb.gravityScale = 0.55f;
    }
 
}
