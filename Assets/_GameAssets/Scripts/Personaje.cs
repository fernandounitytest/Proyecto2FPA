using UnityEngine;

public class Personaje : MonoBehaviour {
    enum Estado { Idle, ToRight, ToLeft};
    [SerializeField] int vidaMaxima;
    bool estaVivo = true;
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


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
