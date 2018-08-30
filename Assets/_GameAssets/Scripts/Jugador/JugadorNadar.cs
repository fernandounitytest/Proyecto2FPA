using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorNadar : MonoBehaviour {

    [SerializeField] float fuerzaLateral = 5;
    [SerializeField] float fuerzaSalto = 10;

    [SerializeField] LayerMask capaAgua;

    [SerializeField] Vector3 posicionCabeza;
    [SerializeField] float radioCabeza;

    Rigidbody2D miRigidbody;
    Animator miAnimator;

    private void Awake()
    {
        miRigidbody = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        miAnimator.SetBool("enAgua", true);
    }

    private void OnDisable()
    {
        miAnimator.SetBool("enAgua", false);
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        miRigidbody.AddForce(Vector2.right * fuerzaLateral * xInput);

        if (xInput < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (xInput > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            miRigidbody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    public bool EstoySumergido()
    {
        Vector3 posicionCabezaGlobal = transform.TransformPoint(posicionCabeza);
        Collider2D agua = Physics2D.OverlapCircle(posicionCabezaGlobal, radioCabeza, capaAgua);

        return (agua != null);
    }
}
