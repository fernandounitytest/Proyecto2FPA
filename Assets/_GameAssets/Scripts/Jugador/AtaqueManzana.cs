using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtaqueManzana : MonoBehaviour {


    [SerializeField] Rigidbody2D prefabManzana;
    [SerializeField] float fuerzaDisparo = 5;

    [SerializeField] Transform puntoDisparo;
    [SerializeField] float tiempoEntreDisparos = 0.5f;
    [SerializeField] Image indicadorManzanaUI;
    Rigidbody2D nuevaManzana;

    float tiempoUltimoDisparo;

    private void Awake()
    {
        int itemActivado = PlayerPrefs.GetInt("itemManzana", 0);
        indicadorManzanaUI.color = new Color32(255, 255, 255, 255);
        if (itemActivado == 0)
        {
            this.enabled = false;
            indicadorManzanaUI.color = new Color32(0, 0, 0, 255);
        }
    }

    public void Activar()
    {
        this.enabled = true;
        PlayerPrefs.SetInt("itemManzana", 1);
        indicadorManzanaUI.color = new Color32(255, 255, 255, 255);
    }

    public void Desactivar()
    {
        this.enabled = false;
        PlayerPrefs.SetInt("itemManzana", 0);
        indicadorManzanaUI.color = new Color32(0, 0, 0, 255);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Municion"))
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
            nuevaManzana = Instantiate(prefabManzana,
                                                     position: puntoDisparo.position,
                                                     rotation: Quaternion.identity);
            float escalaX = this.transform.localScale.x;
            Vector3 vectorLanzamiento = new Vector3(1 * escalaX * fuerzaDisparo, 5);
            nuevaManzana.AddForce(vectorLanzamiento, ForceMode2D.Impulse);
            Invoke("DestruirManzana", 3);
        }

    }
    private void DestruirManzana()
    {
        if (nuevaManzana != null)
        {
            Destroy(nuevaManzana.gameObject);
        }
    }

}
