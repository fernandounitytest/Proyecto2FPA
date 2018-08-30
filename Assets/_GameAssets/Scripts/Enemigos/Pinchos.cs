using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchos : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            other.GetComponent<Jugador>().RecibirEmpujon(desdePosicion: this.transform.position);
            other.GetComponent<Jugador>().RecibirDaño();
        }
    }
}
