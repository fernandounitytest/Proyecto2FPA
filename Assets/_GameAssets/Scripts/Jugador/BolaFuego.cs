using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BolaFuego : MonoBehaviour {

    [SerializeField] float tiempoVida = 3;

    void Start()
    {
        Destroy(this.gameObject, tiempoVida);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidbodyImpactado = collision.collider.attachedRigidbody;
        if (rigidbodyImpactado != null)
        {

            Enemigo enemigoImpactado = rigidbodyImpactado.GetComponent<Enemigo>();
            if (enemigoImpactado != null)
            {
                enemigoImpactado.RecibirDaño(1);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(Destruirme);

        Rigidbody2D rigidbodyImpactado = other.attachedRigidbody;
        if (rigidbodyImpactado != null)
        {
            Enemigo enemigoImpactado = rigidbodyImpactado.GetComponent<Enemigo>();
            if (enemigoImpactado != null)
            {
                enemigoImpactado.RecibirDaño();
            }
        }
    }

    void Destruirme()
    {
        Destroy(this.gameObject);
    }
}
