using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Enemigo : Personaje {

    [SerializeField] ParticleSystem prefabExplosion;

    protected Animator miAnimator;
    protected Rigidbody2D miRigidbody;

    protected override void Awake()
    {
        base.Awake();

        miAnimator = GetComponent<Animator>();
        miRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (this.EstaVivo && collision.collider.CompareTag("Player"))
        {
            Jugador jugador = collision.collider.GetComponent<Jugador>();
            jugador.RecibirEmpujon(desdePosicion: this.transform.position);
            jugador.RecibirDaño();
        }
        */
    }

    protected override void Morir()
    {
        if (prefabExplosion != null)
        {
            ParticleSystem nuevaExplosion = Instantiate(
            prefabExplosion, this.transform.position, Quaternion.identity);

            Destroy(nuevaExplosion.gameObject, nuevaExplosion.main.duration);
        }

        this.enabled = false;
        miAnimator.SetTrigger("morir");//Es un trigger para la transición y mostrar la animación de morir
        miRigidbody.isKinematic = false;

        Tweener animacionFade = this.GetComponent<SpriteRenderer>().DOFade(0, duration: 1);
        animacionFade.SetDelay(1);
        animacionFade.OnComplete(Destruirme);
    }

    void Destruirme()
    {
        Destroy(this.gameObject);
    }

}
