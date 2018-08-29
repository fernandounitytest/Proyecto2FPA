using UnityEngine;

public abstract class Personaje : MonoBehaviour {
    enum Estado { Idle, ToRight, ToLeft};
    [SerializeField] int vidaMaxima = 1;
    int vidaActual;

    public int VidaMaxima
    {
        get
        {
            return vidaMaxima;
        }
    }

    public int VidaActual
    {
        get
        {
            return vidaActual;
        }
        set
        {
            vidaActual = Mathf.Clamp(value, 0, VidaMaxima);
            if (vidaActual == 0)
            {
                Morir();
            }
        }
    }

    public bool EstaVivo
    {
        get
        {
            return vidaActual > 0;
        }
    }

    public bool EstoyDañado
    {
        get
        {
            return vidaActual < vidaMaxima;
        }
    }

    protected virtual void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public virtual void RecibirDaño(int daño = 1)
    {
        vidaActual -= daño;
        vidaActual = Mathf.Max(vidaActual, 0);

        if (vidaActual == 0)
        {
            Morir();
        }
    }

    public void Curar(int curacion = 1)
    {
        vidaActual += curacion;
        vidaActual = Mathf.Min(vidaActual, vidaMaxima);
    }

    protected abstract void Morir();
}
