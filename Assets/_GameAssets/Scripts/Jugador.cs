using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : Personaje {

    Animator miAnimator;
    Rigidbody2D miRigidbody;
    [SerializeField] float velocidadX = 5;
    [SerializeField] float velocidadSalto = 10;

    bool enSuelo=true;
    bool saltando = false;



    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        miAnimator = GetComponent<Animator>();
        miRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update () {
        ComprobarSalto();
    }

    private void FixedUpdate()
    {
        Mover();
    }

    private void Mover()
    {
        float xInput = Input.GetAxis("Horizontal");
        if (xInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //miAnimator.SetFloat("xInput", xInput);
        

        Vector2 velocidad = miRigidbody.velocity;
        velocidad.x = xInput * velocidadX;
        miRigidbody.velocity = velocidad;
        if (xInput != 0)
        {
            miAnimator.SetBool("taberneroAndando", true);
        } else
        {
            miAnimator.SetBool("taberneroAndando", false);
        } 
    }

    private void ComprobarSalto()
    {
        if (enSuelo && Input.GetButtonDown("Jump"))
        {
            saltando = true;

            Saltar();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            saltando = false;
        }
    }

    private void Saltar()
    {
        Vector2 velocidad = miRigidbody.velocity;
        velocidad.y = velocidadSalto;
        miRigidbody.velocity = velocidad;
    }

    protected override void Morir()
    {
        throw new System.NotImplementedException();
    }
}
