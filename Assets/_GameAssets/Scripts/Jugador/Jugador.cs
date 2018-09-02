using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Jugador : Personaje {

    [SerializeField] float limiteCaidaY = -10;

    [SerializeField] RuntimeAnimatorController[] controladoresColores;

    Transform ultimoPuntoSpawn;

    Rigidbody2D miRigidbody;
    Animator miAnimator;

    AtaqueManzana miAtaqueManzana;

    JugadorCaminar miMovimientoCaminar;
    JugadorNadar miMovimientoNadar;

    public bool inmunidadTemporal;

    public Transform UltimoPuntoSpawn
    {
        get
        {
            return ultimoPuntoSpawn;
        }

        set
        {
            ultimoPuntoSpawn = value;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        miAnimator = GetComponent<Animator>();
        miRigidbody = GetComponent<Rigidbody2D>();

        miMovimientoCaminar = GetComponent<JugadorCaminar>();

        miMovimientoNadar = GetComponent<JugadorNadar>();
        miMovimientoNadar.enabled = false;

        miAtaqueManzana = GetComponent<AtaqueManzana>();

    }

    private void Start()
    {
        CrearCheckpointInicial();

        PlayerPrefs.SetString("ultimoNivel", SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        ComprobarLimiteCaida();
        ComprobarAtaque();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            bool estoySumergido = miMovimientoNadar.EstoySumergido();

            if (miMovimientoCaminar.enabled && estoySumergido)
            {
                miMovimientoCaminar.enabled = false;
                miMovimientoNadar.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Agua"))
        {
            miMovimientoCaminar.enabled = true;
            miMovimientoNadar.enabled = false;
        }
    }

    private void CrearCheckpointInicial()
    {
        //GameObject checkPointInicial = new GameObject("Checkpoint Inicial");
        //checkPointInicial.transform.position = this.transform.position;
        UltimoPuntoSpawn = this.transform;
    }

    private void ComprobarLimiteCaida()
    {
        if (this.transform.position.y < limiteCaidaY)
        {
            RecibirDaño();
            ResetearEnUltimoPuntoSpawn();
        }
    }

    private void ComprobarAtaque()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            miAtaqueManzana.Disparar();
        }
    }

    void ResetearEnUltimoPuntoSpawn()
    {
        Debug.Log("resetando");
        this.transform.position = ultimoPuntoSpawn.transform.position;
        miRigidbody.velocity = Vector3.zero;
        ResetearEscenario();
    }

    void ResetearEscenario()
    {
        ElementoReseteable[] reseteables = FindObjectsOfType<ElementoReseteable>();
        for (int i = 0; i < reseteables.Length; i++)
        {
            reseteables[i].Reset();
        }
    }

    public override void RecibirDaño(int daño = 1)
    {
        if (inmunidadTemporal) { return; }

        base.RecibirDaño(daño);
        ActivarInmunidadTemporal();

        miAtaqueManzana.Desactivar();
    }

    public void RecibirEmpujon(Vector3 desdePosicion)
    {

        if (inmunidadTemporal) { return; }

        // FIXME: El empujon debería hacerse según el tipo de movimiento
        // que esté activo en cada momento.
        miMovimientoCaminar.RecibirEmpujon(desdePosicion);
    }

    void ActivarInmunidadTemporal()
    {
        inmunidadTemporal = true;

        Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("CapaEnemigos"),
                LayerMask.NameToLayer("Jugador"),
                ignore: true);

        Sequence secuenciaParpadeo = DOTween.Sequence();
        secuenciaParpadeo.Append(GetComponent<SpriteRenderer>().DOFade(0, duration: 0));
        secuenciaParpadeo.AppendInterval(0.05f);
        secuenciaParpadeo.Append(GetComponent<SpriteRenderer>().DOFade(1, duration: 0));
        secuenciaParpadeo.AppendInterval(0.15f);
        secuenciaParpadeo.SetLoops(10);

        Invoke("CancelarInmunidadTemporal", 2);
    }

    void CancelarInmunidadTemporal()
    {
        inmunidadTemporal = false;

        Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("CapaEnemigos"),
                LayerMask.NameToLayer("Jugador"),
                ignore: false);
    }

    protected override void Morir()
    {
        SceneManager.LoadScene("GameOverScene");
    }


}
