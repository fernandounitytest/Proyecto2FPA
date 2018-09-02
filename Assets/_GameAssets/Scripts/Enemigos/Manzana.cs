using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manzana : MonoBehaviour {
    [SerializeField] GameObject enemigo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemigo enemigo = collision.gameObject.GetComponent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.RecibirDaño();
        }
        Destroy(this.gameObject);
    }
}
