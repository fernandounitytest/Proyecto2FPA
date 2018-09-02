using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoTerrestre : Enemigo {
    private bool vivo = true;
    [SerializeField] int speed = 1;
    [SerializeField] int distance = 20;
    private int currentDistance = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        currentDistance++;
        if (currentDistance >= distance)
        {
            speed = speed * -1;
            currentDistance = 0;

            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
    }
    protected override void Morir()
    {
        if (vivo)
        {
            vivo = false;
            this.enabled = false;
            miRigidbody.isKinematic = false;
            ParticleSystem ps = Instantiate(prefabExplosion, this.transform.position, Quaternion.identity);
            ps.Play();
            AudioSource bang = GetComponent<AudioSource>();
            bang.Play();
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(ps.gameObject, ps.main.duration);
            Destroy(this.gameObject, ps.main.duration);
        }
    }
}
