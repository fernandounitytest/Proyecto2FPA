using UnityEngine;

public class Personaje : MonoBehaviour {
    enum Estado { Idle, ToRight, ToLeft};
    [SerializeField] int vidaMaxima;
    private int vidaActual;
    
    public int GetVidaMaxima()
    {
        return vidaMaxima;
    }

    public void SetVidaMaxima(int value)
    {
        vidaMaxima = value;
    }

    public int GetVidaActual()
    {
        return vidaActual;
    }

    public void SetVidaActual(int value)
    {
        vidaActual = value;
    }

    public void RecibirDaño(int daño = 1)
    {
        vidaActual -= daño;
        vidaActual = Mathf.Max(vidaActual, 0);
    }

    public void Curar(int curacion = 1)
    {
        vidaActual += curacion;
        vidaActual = Mathf.Min(vidaActual, vidaMaxima);
    }
    
    public bool EstoyVivo()
    {
        return vidaActual > 0;
    }

    public bool EstoyDañado()
    {
        return vidaActual < vidaMaxima;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
