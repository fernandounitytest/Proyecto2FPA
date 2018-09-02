using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Jugador j = collision.GetComponent<Jugador>();
        if (j!=null)
        {
            j.UltimoPuntoSpawn = this.transform;
        }
    }

}
