using UnityEngine;

public class Personaje : MonoBehaviour {
    enum Estado { Idle, ToRight, ToLeft};
    [SerializeField] int vidaMaxima;
    private int vidaActual;

    public int VidaActual
    {
        get
        {
            return vidaActual;
        }

        set
        {
            vidaActual = value;
        }
    }

    public int VidaMaxima
    {
        get
        {
            return vidaMaxima;
        }

        set
        {
            vidaMaxima = value;
        }
    }

    public void RecibirDaño(int daño = 1)
    {
        VidaActual -= daño;
        VidaActual = Mathf.Max(VidaActual, 0);
    }

    public void Curar(int curacion = 1)
    {
        VidaActual += curacion;
        VidaActual = Mathf.Min(VidaActual, VidaMaxima);
    }
    
    public bool EstoyVivo()
    {
        return VidaActual > 0;
    }

    public bool EstoyDañado()
    {
        return VidaActual < VidaMaxima;
    }

    void Start () {
		
	}

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
