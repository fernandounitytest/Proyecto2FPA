using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Enemigo : Personaje {

    [SerializeField] public ParticleSystem prefabExplosion;

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
        if (this.EstaVivo && collision.collider.CompareTag("Jugador"))
        {
            Jugador jugador = collision.collider.GetComponent<Jugador>();
            jugador.RecibirEmpujon(desdePosicion: this.transform.position);
            jugador.RecibirDaño();
        }
    }

    protected override void Morir()
    {
        this.enabled = false;
        miRigidbody.isKinematic = false;

        Tweener animacionFade = this.GetComponent<SpriteRenderer>().DOFade(0, duration: 1);
        animacionFade.SetDelay(1);
        animacionFade.OnComplete(Destruirme);
    }

    protected void Destruirme()
    {
        ParticleSystem ps = Instantiate(prefabExplosion, this.transform.position, Quaternion.identity);
        ps.Play();
        AudioSource bang = GetComponent<AudioSource>();
        bang.Play();
        Destroy(ps.gameObject, ps.main.duration);
        Destroy(this.gameObject, ps.main.duration);
    }

}
