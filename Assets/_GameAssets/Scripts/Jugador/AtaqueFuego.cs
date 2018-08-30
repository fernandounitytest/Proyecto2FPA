using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueFuego : MonoBehaviour {


    [SerializeField] Rigidbody2D prefabBolaFuego;
    [SerializeField] float fuerzaDisparo = 10;
    [SerializeField] float fuerzaRotacion = 10;

    [SerializeField] Vector3 puntoDisparo;
    [SerializeField] float tiempoEntreDisparos = 0.5f;

    float tiempoUltimoDisparo;

    private void Awake()
    {
        int itemActivado = PlayerPrefs.GetInt("itemFuego", 0);
        if (itemActivado == 0)
        {
            this.enabled = false;
        }
    }

    public void Activar()
    {
        this.enabled = true;
        PlayerPrefs.SetInt("itemFuego", 1);
    }

    public void Desactivar()
    {
        this.enabled = false;
        PlayerPrefs.SetInt("itemFuego", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ItemFuego"))
        {
            Activar();
            Destroy(collision.gameObject);
        }
    }

    public void Disparar()
    {
        if (this.isActiveAndEnabled && Time.time > tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            tiempoUltimoDisparo = Time.time;

            Vector3 puntoDisparoGlobal = transform.TransformPoint(puntoDisparo);

            Rigidbody2D nuevaBolaFuego = Instantiate(prefabBolaFuego,
                                                     position: puntoDisparoGlobal,
                                                     rotation: Quaternion.identity);
            float escalaX = this.transform.localScale.x;
            nuevaBolaFuego.AddForce(Vector3.right * escalaX * fuerzaDisparo, ForceMode2D.Impulse);
            nuevaBolaFuego.AddTorque(fuerzaRotacion, ForceMode2D.Impulse);
        }

    }

}
