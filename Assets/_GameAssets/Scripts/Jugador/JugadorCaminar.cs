using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorCaminar : MonoBehaviour {


    [SerializeField] float velocidadX = 5;
    [SerializeField] float velocidadSalto = 10;
    [SerializeField] float fuerzaSaltoProlongado = 5;

    [SerializeField] Transform posicionPies;
    [SerializeField] float radioPies = 0.3f;
    [SerializeField] LayerMask capaSuelo;
    [SerializeField] LayerMask capaEnemigos;

    [SerializeField] float gravedadPegaSuelo = 50;

    [SerializeField] Vector2 velocidadEmpujon = new Vector2(10, 10);

    float gravedadInicial;

    bool saltando;
    bool enSuelo;
    bool empujado;

    Animator miAnimator;
    Rigidbody2D miRigidbody;

    public bool GetEnSuelo()
    {
        return enSuelo;
    }

    protected void Awake()
    {
        miAnimator = GetComponent<Animator>();
        miRigidbody = GetComponent<Rigidbody2D>();

        gravedadInicial = miRigidbody.gravityScale;
    }

    private void FixedUpdate()
    {
        Moverse();
        ProlongarSalto();

        ComprobarSuelo();
        ComprobarEnemigo();

        BloquearDeslizamiento();
    }

    private void Update()
    {
        ComprobarSalto();
    }

    private void Moverse()
    {
        if (empujado) { return; }

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

        //miRigidbody.velocity.x = xInput * velocidadX;
        if (xInput > 0 || xInput < 0)
        {
            miAnimator.SetBool("taberneroAndando", true);
        } else
        {
            miAnimator.SetBool("taberneroAndando", false);
        }

        Vector2 velocidad = miRigidbody.velocity;
        velocidad.x = xInput * velocidadX;
        miRigidbody.velocity = velocidad;


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

    private void ProlongarSalto()
    {
        if (saltando)
        {
            miRigidbody.AddForce(Vector2.up * fuerzaSaltoProlongado);
        }
    }

    private void ComprobarSuelo()
    {
        Collider2D colision = Physics2D.OverlapCircle(posicionPies.position, radioPies, capaSuelo);
        enSuelo = (colision != null);

        if (empujado && enSuelo && miRigidbody.velocity.y < 0)
        {
            empujado = false;
        }
        miAnimator.SetBool("enSuelo", enSuelo);
        Debug.Log("Ensuelo:" + enSuelo);
    }

    private void ComprobarEnemigo()
    {
        Collider2D colision = Physics2D.OverlapCircle(posicionPies.position, radioPies, capaEnemigos);
        if (colision != null)
        {
            Enemigo enemigo = colision.GetComponent<Enemigo>();
            enemigo.RecibirDaño();

            Saltar();
        }
    }

    private void BloquearDeslizamiento()
    {
        float xInput = Input.GetAxis("Horizontal");
        if (enSuelo && !saltando && !empujado && Mathf.Abs(xInput) < 0.01f)
        {
            if (miRigidbody.velocity.y > 1f)
            {
                miRigidbody.gravityScale = gravedadPegaSuelo;
            }
            else { miRigidbody.gravityScale = gravedadInicial; }

            miRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            miRigidbody.gravityScale = gravedadInicial;
            miRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void RecibirEmpujon(Vector3 desdePosicion)
    {
        empujado = true;
        Invoke("CancelarEmpujon", 1);

        miRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (desdePosicion.x < this.transform.position.x)
        {
            Vector2 velocidad = new Vector2(velocidadEmpujon.x, velocidadEmpujon.y);
            miRigidbody.velocity = velocidad;
        }
        else
        {
            Vector2 velocidad = new Vector2(-velocidadEmpujon.x, velocidadEmpujon.y);
            miRigidbody.velocity = velocidad;
        }
    }

    void CancelarEmpujon()
    {
        empujado = false;
    }

    
}
