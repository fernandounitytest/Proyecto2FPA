using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuenteTrampa : ElementoReseteable {
    [SerializeField] float tiempoCaida = 1;

    Rigidbody2D miRigidbody;
    bool enCaida = false;

    Vector3 posicionInicial;
    Vector3 rotacionInicial;

    private void Awake()
    {
        miRigidbody = GetComponent<Rigidbody2D>();

        posicionInicial = this.transform.position;
        rotacionInicial = this.transform.eulerAngles;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Jugador"))
        {
            Vector3 posicionJugador = collision.transform.position;
            bool jugadorPorEncima = posicionJugador.y > this.transform.position.y;

            if (jugadorPorEncima && !enCaida)
            {
                enCaida = true;
                Invoke("ActivarGravedad", tiempoCaida);
            }
        }
    }

    void ActivarGravedad()
    {
        miRigidbody.isKinematic = false;
    }

    public override void Reset()
    {
        enCaida = false;
        miRigidbody.isKinematic = true;
        miRigidbody.velocity = Vector2.zero;
        miRigidbody.angularVelocity = 0;

        CancelInvoke();

        this.transform.position = posicionInicial;
        this.transform.eulerAngles = rotacionInicial;
    }
}
