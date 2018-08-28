using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJuego : MonoBehaviour {

    [SerializeField] Image prototipoIconoVida;
    [SerializeField] Transform panelVidas;

    [SerializeField] Sprite spriteVida;

    Image[] iconosVida;
    Jugador jugador;
    int ultimaVidaActual;

    void Awake()
    {
        jugador = GameObject.FindWithTag("Jugador").GetComponent<Jugador>();
        ultimaVidaActual = jugador.VidaActual;

        int vidaMaxima = jugador.VidaMaxima;
        iconosVida = new Image[vidaMaxima];

        for (int i = 0; i < vidaMaxima; i++)
        {
            Image nuevoIconoVida = Instantiate(prototipoIconoVida);
            nuevoIconoVida.transform.SetParent(panelVidas, false);
            nuevoIconoVida.gameObject.SetActive(true);

            iconosVida[i] = nuevoIconoVida;
        }

        ActualizarCorazones();
    }

    void Update()
    {
        if (jugador.VidaActual != ultimaVidaActual)
        {
            ultimaVidaActual = jugador.VidaActual;
            ActualizarCorazones();
        }
    }

    private void ActualizarCorazones()
    {
        int vidaActual = jugador.VidaActual;
        int vidaMaxima = jugador.VidaMaxima;

        for (int i = 0; i < vidaActual; i++)
        {
            iconosVida[i].sprite = spriteVida;
        }
        for (int i = vidaActual; i < vidaMaxima; i++)
        {
            //iconosVida[i].sprite = spriteCorazonVacio;
            iconosVida[i].sprite = spriteVida;
        }
    }
}
